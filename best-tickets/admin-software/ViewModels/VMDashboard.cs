using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using ticketlibrary.Models;
using admintickets.Context;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace admintickets.ViewModels
{
    public class VMDashboard : ObservableObject
    {
        private readonly BestTicketContext _context;
        
        // Dashboard Statistics
        private int _totalTickets;
        public int TotalTickets
        {
            get => _totalTickets;
            set => SetProperty(ref _totalTickets, value);
        }
        
        private int _paidTickets;
        public int PaidTickets
        {
            get => _paidTickets;
            set => SetProperty(ref _paidTickets, value);
        }
        
        private int _activeSubscriptions;
        public int ActiveSubscriptions
        {
            get => _activeSubscriptions;
            set => SetProperty(ref _activeSubscriptions, value);
        }
        
        private decimal _totalRevenue;
        public decimal TotalRevenue
        {
            get => _totalRevenue;
            set => SetProperty(ref _totalRevenue, value);
        }

        private decimal _todayRevenue;
        public decimal TodayRevenue
        {
            get => _todayRevenue;
            set => SetProperty(ref _todayRevenue, value);
        }
        
        // Charts
        private SeriesCollection _ticketsOverTimeCollection;
        public SeriesCollection TicketsOverTimeCollection
        {
            get => _ticketsOverTimeCollection;
            set => SetProperty(ref _ticketsOverTimeCollection, value);
        }
        
        public List<string> TicketsOverTimeLabels { get; set; }

        private SeriesCollection _ticketStatusCollection;
        public SeriesCollection TicketStatusCollection
        {
            get => _ticketStatusCollection;
            set => SetProperty(ref _ticketStatusCollection, value);
        }
        
        private SeriesCollection _revenueByHospitalCollection;
        public SeriesCollection RevenueByHospitalCollection
        {
            get => _revenueByHospitalCollection;
            set => SetProperty(ref _revenueByHospitalCollection, value);
        }
        
        public List<string> RevenueByHospitalLabels { get; set; } = new List<string>();

        private SeriesCollection _paymentMethodsCollection;
        public SeriesCollection PaymentMethodsCollection
        {
            get => _paymentMethodsCollection;
            set => SetProperty(ref _paymentMethodsCollection, value);
        }

        /// <summary>
        /// Formatter for displaying revenue values as currency
        /// </summary>
        public Func<double, string> RevenueFormatter { get; set; }

        public VMDashboard()
        {
            _context = new BestTicketContext();
            TicketsOverTimeLabels = new List<string>();
            LoadDataAsync().ConfigureAwait(false);
            
            // Initialize the revenue formatter to display values as euros with 2 decimal places
            RevenueFormatter = value => string.Format("{0:C}", value);
        }
        
        public async Task LoadDataAsync()
        {
            await LoadStatisticsAsync();
            await LoadTicketsOverTimeChartAsync();
            await LoadTicketStatusChartAsync();
            await LoadRevenueByHospitalChartAsync();
            await LoadPaymentMethodsChartAsync();
        }
        
        private async Task LoadStatisticsAsync()
        {
            // Load basic statistics
            TotalTickets = await _context.Ticket.CountAsync();
            PaidTickets = await _context.Ticket.CountAsync(t => t.PaymentTime.HasValue);
            
            var now = DateTime.Now;
            ActiveSubscriptions = await _context.Subscription.CountAsync(s => 
                s.DateStart <= now && s.DateEnd >= now);
            
            TotalRevenue = await _context.TicketPayment.SumAsync(p => p.PaymentAmountTotal);
            TotalRevenue += await _context.Subscription.SumAsync(s => s.PricePaid);
            
            TodayRevenue = await _context.TicketPayment
                .Join(_context.Ticket, 
                    tp => tp.Id, 
                    t => t.TicketPaymentId, 
                    (tp, t) => new { Payment = tp, Ticket = t })
                .Where(x => x.Ticket.PaymentTime.HasValue && 
                       x.Ticket.PaymentTime.Value.Date == DateTime.Today)
                .SumAsync(x => x.Payment.PaymentAmountTotal);
            
            TodayRevenue += await _context.Subscription
                .Where(s => s.DateStart.Date <= DateTime.Today && 
                       s.DateEnd.Date >= DateTime.Today)
                .SumAsync(s => s.PricePaid);
        }
        
        private async Task LoadTicketsOverTimeChartAsync()
        {
            // Get tickets for the last 7 days
            var endDate = DateTime.Now.Date;
            var startDate = endDate.AddDays(-6);
            
            var dailyTickets = await _context.Ticket
                .Where(t => t.CreationTime.Date >= startDate && t.CreationTime.Date <= endDate)
                .GroupBy(t => t.CreationTime.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .OrderBy(x => x.Date)
                .ToListAsync();
            
            // Prepare data for chart
            var dates = new List<DateTime>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }
            
            var createdSeries = new ChartValues<int>();
            var paidSeries = new ChartValues<int>();
            
            // Get paid tickets for the same period
            var dailyPaidTickets = await _context.Ticket
                .Where(t => t.PaymentTime.HasValue && 
                       t.PaymentTime.Value.Date >= startDate && 
                       t.PaymentTime.Value.Date <= endDate)
                .GroupBy(t => t.PaymentTime.Value.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .OrderBy(x => x.Date)
                .ToListAsync();
            
            TicketsOverTimeLabels.Clear();
            
            foreach (var date in dates)
            {
                TicketsOverTimeLabels.Add(date.ToString("MM/dd"));
                
                var createdCount = dailyTickets
                    .FirstOrDefault(x => x.Date == date)?.Count ?? 0;
                createdSeries.Add(createdCount);
                
                var paidCount = dailyPaidTickets
                    .FirstOrDefault(x => x.Date == date)?.Count ?? 0;
                paidSeries.Add(paidCount);
            }
            
            TicketsOverTimeCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Created Tickets",
                    Values = createdSeries,
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10
                },
                new LineSeries
                {
                    Title = "Paid Tickets",
                    Values = paidSeries,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10
                }
            };
        }
        
        private async Task LoadTicketStatusChartAsync()
        {
            var paidCount = await _context.Ticket.CountAsync(t => t.PaymentTime.HasValue);
            var unpaidCount = await _context.Ticket.CountAsync(t => !t.PaymentTime.HasValue);
            
            TicketStatusCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Paid",
                    Values = new ChartValues<int> { paidCount },
                    Fill = new SolidColorBrush(Colors.Green)
                },
                new PieSeries
                {
                    Title = "Unpaid",
                    Values = new ChartValues<int> { unpaidCount },
                    Fill = new SolidColorBrush(Colors.Red)
                }
            };
        }
        
        private async Task LoadRevenueByHospitalChartAsync()
        {
            // Get all hospitals with either ticket or subscription revenue
            var hospitals = await _context.Hospital
                .Select(h => new { h.Id, h.Name })
                .ToListAsync();

            // Get ticket payment revenue by hospital
            var ticketRevenue = await _context.Ticket
                .Where(t => t.TicketPayment != null)
                .Include(t => t.TicketPayment)
                .GroupBy(t => t.HospitalId)
                .Select(g => new 
                { 
                    HospitalId = g.Key,
                    Revenue = g.Sum(t => t.TicketPayment.PaymentAmountTotal) 
                })
                .ToDictionaryAsync(x => x.HospitalId, x => x.Revenue);
            
            // Get subscription revenue by hospital
            var subscriptionRevenue = await _context.Subscription
                .GroupBy(s => s.HospitalId)
                .Select(g => new
                {
                    HospitalId = g.Key,
                    Revenue = g.Sum(s => s.PricePaid)
                })
                .ToDictionaryAsync(x => x.HospitalId, x => x.Revenue);
            
            // Combine all revenue data
            var hospitalRevenue = hospitals
                .Select(h => new {
                    HospitalName = h.Name,
                    TicketRevenue = ticketRevenue.ContainsKey(h.Id) ? ticketRevenue[h.Id] : 0,
                    SubscriptionRevenue = subscriptionRevenue.ContainsKey(h.Id) ? subscriptionRevenue[h.Id] : 0
                })
                .Select(x => new {
                    HospitalName = x.HospitalName,
                    TicketRevenue = x.TicketRevenue,
                    SubscriptionRevenue = x.SubscriptionRevenue,
                    TotalRevenue = x.TicketRevenue + x.SubscriptionRevenue
                })
                .Where(x => x.TotalRevenue > 0) // Only include hospitals with revenue
                .OrderByDescending(x => x.TotalRevenue)
                .Take(5)
                .ToList();
            
            RevenueByHospitalCollection = new SeriesCollection();
            RevenueByHospitalLabels.Clear();
            
            // Add hospital names as labels for X-axis
            foreach (var hospital in hospitalRevenue)
            {
                RevenueByHospitalLabels.Add(hospital.HospitalName);
            }
            
            // Add ticket revenue series (bottom part of stack)
            RevenueByHospitalCollection.Add(new StackedColumnSeries
            {
                Title = "Ticket Revenue",
                Values = new ChartValues<decimal>(hospitalRevenue.Select(h => h.TicketRevenue)),
                Fill = new SolidColorBrush(Colors.DodgerBlue)
            });
            
            // Add subscription revenue series (top part of stack)
            RevenueByHospitalCollection.Add(new StackedColumnSeries
            {
                Title = "Subscription Revenue",
                Values = new ChartValues<decimal>(hospitalRevenue.Select(h => h.SubscriptionRevenue)),
                Fill = new SolidColorBrush(Colors.MediumSeaGreen)
            });
        }
        
        private async Task LoadPaymentMethodsChartAsync()
        {
            // First get all payment methods with counts
            var rawPaymentMethods = await _context.TicketPayment
                .GroupBy(p => p.PaymentMethod)
                .Select(g => new { Method = g.Key, Count = g.Count() })
                .ToListAsync();
            
            // Consolidate subscription payments (XXXX-XXXX pattern)
            var subscriptionCount = 0;
            var processedMethods = new Dictionary<string, int>();
            
            foreach (var item in rawPaymentMethods)
            {
                // Check for XXXX-XXXX pattern (subscription)
                if (item.Method != null && 
                    item.Method.Length == 9 && 
                    item.Method[4] == '-' && 
                    char.IsLetterOrDigit(item.Method[0]))
                {
                    subscriptionCount += item.Count;
                }
                else
                {
                    if (!processedMethods.ContainsKey(item.Method))
                        processedMethods[item.Method] = 0;
                    
                    processedMethods[item.Method] += item.Count;
                }
            }
            
            // Add the subscription aggregate if any found
            if (subscriptionCount > 0)
                processedMethods["Subscription"] = subscriptionCount;
            
            PaymentMethodsCollection = new SeriesCollection();
            
            // Create chart series from processed data
            foreach (var method in processedMethods.OrderByDescending(x => x.Value))
            {
                PaymentMethodsCollection.Add(new PieSeries
                {
                    Title = method.Key,
                    Values = new ChartValues<int> { method.Value }
                });
            }
        }
        
        public IAsyncRelayCommand RefreshCommand => new AsyncRelayCommand(LoadDataAsync);
    }
}
