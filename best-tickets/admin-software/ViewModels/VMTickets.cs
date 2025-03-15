using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ticketlibrary.Models;
using admintickets.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using admintickets.Views;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace admintickets.ViewModels
{
    public class VMTickets : ObservableObject
    {
        private BestTicketContext context;

        private ObservableCollection<Ticket> _tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get => _tickets;
            set => SetProperty(ref _tickets, value);
        }

        // Pagination properties
        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        private int _itemsPerPage = 10;
        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set => SetProperty(ref _itemsPerPage, value);
        }

        private int _totalPages;
        public int TotalPages
        {
            get => _totalPages;
            set => SetProperty(ref _totalPages, value);
        }

        private int _totalItems;
        public int TotalItems
        {
            get => _totalItems;
            set => SetProperty(ref _totalItems, value);
        }

        // Sorting properties
        private string _sortColumn = "CreationTime";
        public string SortColumn
        {
            get => _sortColumn;
            set 
            { 
                if (SetProperty(ref _sortColumn, value))
                    RefreshCommand.Execute(null);
            }
        }

        private bool _sortDescending = true;
        public bool SortDescending
        {
            get => _sortDescending;
            set 
            { 
                if (SetProperty(ref _sortDescending, value))
                    RefreshCommand.Execute(null);
            }
        }

        // Filter properties
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        private string _paymentStatus = "All";
        public string PaymentStatus
        {
            get => _paymentStatus;
            set => SetProperty(ref _paymentStatus, value);
        }

        public List<string> PaymentStatusOptions { get; } = new List<string> { "All", "Paid", "Unpaid" };

        public VMTickets()
        {
            context = new BestTicketContext();
        }

        public ICommand RefreshCommand => new AsyncRelayCommand(Refresh);
        public ICommand NextPageCommand => new AsyncRelayCommand(NextPage);
        public ICommand PreviousPageCommand => new AsyncRelayCommand(PreviousPage);
        public ICommand FirstPageCommand => new AsyncRelayCommand(FirstPage);
        public ICommand LastPageCommand => new AsyncRelayCommand(LastPage);
        public ICommand ApplyFiltersCommand => new AsyncRelayCommand(Refresh);
        public ICommand ClearFiltersCommand => new AsyncRelayCommand(ClearFilters);
        public ICommand SortCommand => new RelayCommand<string>(SortByColumn);

        private async Task Refresh()
        {
            var query = context.Ticket
                               .Include(t => t.Hospital)
                               .Include(t => t.TicketPayment)
                               .AsQueryable();

            // Apply filters
            query = ApplyFilters(query);

            // Get total count for pagination
            TotalItems = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(TotalItems / (double)ItemsPerPage);

            // Ensure current page is valid
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages && TotalPages > 0) CurrentPage = TotalPages;

            // Apply sorting
            query = ApplySorting(query);

            // Apply pagination
            var items = await query
                .Skip((CurrentPage - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToListAsync();

            Tickets = new ObservableCollection<Ticket>(items);
            OnPropertyChanged(nameof(Tickets));
        }

        private IQueryable<Ticket> ApplyFilters(IQueryable<Ticket> query)
        {
            // Apply text search
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var searchTerm = SearchText.ToLower();
                query = query.Where(t => 
                    t.TicketNumber.ToLower().Contains(searchTerm) ||
                    t.Hospital.Name.ToLower().Contains(searchTerm));
            }

            // Apply date range filter
            if (StartDate.HasValue)
            {
                query = query.Where(t => t.CreationTime >= StartDate.Value);
            }

            if (EndDate.HasValue)
            {
                var endDatePlusOneDay = EndDate.Value.AddDays(1);
                query = query.Where(t => t.CreationTime < endDatePlusOneDay);
            }

            // Apply payment status filter
            if (PaymentStatus == "Paid")
            {
                query = query.Where(t => t.PaymentTime != null);
            }
            else if (PaymentStatus == "Unpaid")
            {
                query = query.Where(t => t.PaymentTime == null);
            }

            return query;
        }

        private IQueryable<Ticket> ApplySorting(IQueryable<Ticket> query)
        {
            switch (SortColumn)
            {
                case "TicketNumber":
                    query = SortDescending 
                        ? query.OrderByDescending(t => t.TicketNumber) 
                        : query.OrderBy(t => t.TicketNumber);
                    break;
                case "HospitalName":
                    query = SortDescending 
                        ? query.OrderByDescending(t => t.Hospital.Name) 
                        : query.OrderBy(t => t.Hospital.Name);
                    break;
                case "CreationTime":
                    query = SortDescending 
                        ? query.OrderByDescending(t => t.CreationTime) 
                        : query.OrderBy(t => t.CreationTime);
                    break;
                case "PaymentTime":
                    query = SortDescending 
                        ? query.OrderByDescending(t => t.PaymentTime) 
                        : query.OrderBy(t => t.PaymentTime);
                    break;
                case "DepartureTime":
                    query = SortDescending 
                        ? query.OrderByDescending(t => t.DepartureTime) 
                        : query.OrderBy(t => t.DepartureTime);
                    break;
                default:
                    query = SortDescending 
                        ? query.OrderByDescending(t => t.CreationTime) 
                        : query.OrderBy(t => t.CreationTime);
                    break;
            }
            return query;
        }

        private async Task NextPage()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                await Refresh();
            }
        }

        private async Task PreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await Refresh();
            }
        }

        private async Task FirstPage()
        {
            CurrentPage = 1;
            await Refresh();
        }

        private async Task LastPage()
        {
            CurrentPage = TotalPages;
            await Refresh();
        }

        private async Task ClearFilters()
        {
            SearchText = null;
            StartDate = null;
            EndDate = null;
            PaymentStatus = "All";
            CurrentPage = 1;
            await Refresh();
        }

        private void SortByColumn(string columnName)
        {
            if (columnName == SortColumn)
            {
                // Toggle sort direction if clicking the same column
                SortDescending = !SortDescending;
            }
            else
            {
                // Default to descending for new column
                SortColumn = columnName;
                SortDescending = true;
            }
        }

        public ICommand ViewPaymentCommand => new RelayCommand<Ticket>((ticket) =>
        {
            if (ticket?.TicketPayment != null)
            {
                // Open a floating window to display read-only ticket payment details.
                TicketPaymentDetailsWindow detailsWindow = new TicketPaymentDetailsWindow(ticket.TicketPayment);
                detailsWindow.ShowDialog();
            }
        });

        public ICommand PrintTicketAsPdfCommand => new AsyncRelayCommand<Ticket>(async (ticket) =>
        {
            if (ticket == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"Ticket_{ticket.Id}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await GenerateTicketPdf(ticket, filePath);
                if (MessageBox.Show("Ticket saved as PDF. Do you want to open it?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    // System.Diagnostics.Process.Start(filePath);
                    // The specified executable is not a valid application for this OS platform.'
                    // so we need to do that :
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
        });

        public ICommand PrintTicketAsPngCommand => new AsyncRelayCommand<Ticket>(async (ticket) =>
        {
            if (ticket == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Files (*.png)|*.png",
                FileName = $"Ticket_{ticket.Id}.png"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await GenerateTicketPng(ticket, filePath);
                if (MessageBox.Show("Ticket saved as PNG. Do you want to open it?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    // System.Diagnostics.Process.Start(filePath);
                    // The specified executable is not a valid application for this OS platform.'
                    // so we need to do that :
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
        });

        public ICommand SetTicketAsPaidCommand => new AsyncRelayCommand<Ticket>(async (ticket) =>
        {
            if (ticket == null || ticket.PaymentTime != null) return;

            ticket.PaymentTime = DateTime.Now;
            ticket.TicketPayment = new TicketPayment
            {
                PaymentAmountTotal = 0,  // Placeholder amount
                PaymentAmountOfTax = 0,
                PaymentAmountBeforeTax = 0,
                PaymentMethod = "admin-validated",
                SubscriptionId = null,
                CodeUsedId = null
            };
            
            await context.SaveChangesAsync();
            OnPropertyChanged(nameof(Tickets));

            await Refresh();
            MessageBox.Show("Ticket set as paid.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        });

        public ICommand SetTicketAsUnpaidCommand => new AsyncRelayCommand<Ticket>(async (ticket) =>
        {
            if (ticket == null || ticket.PaymentTime == null) return;

            ticket.PaymentTime = null;
            ticket.TicketPayment = null;
            
            await context.SaveChangesAsync();
            OnPropertyChanged(nameof(Tickets));

            await Refresh();
            MessageBox.Show("Ticket set as unpaid.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        });

        // Helper methods for PDF/PNG generation
        private async Task GenerateTicketPdf(Ticket ticket, string filePath)
        {
            try {
                var ticketDocument = new ticket_library.Documents.TicketDocument(ticket);
                ticketDocument.GeneratePdf(filePath);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await Task.CompletedTask;
        }

        private async Task GenerateTicketPng(Ticket ticket, string filePath)
        {
            try {
                var ticketDocument = new ticket_library.Documents.TicketDocument(ticket);
                // document.GenerateImages(imageIndex => $"image{imageIndex}.png");
                ticketDocument.GenerateImages(imageIndex => imageIndex == 0 ? filePath : filePath.Replace(".png", $"_{imageIndex}.png"));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error generating PNG: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await Task.CompletedTask;
        }
    }
}
