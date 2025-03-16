using System;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ticketlibrary.Models;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace ticket_library.Documents;


public class TicketDocument : DocumentBase
{
    public Ticket Ticket { get; }

    public TicketDocument(Ticket ticket) : base()
    {
        Ticket = ticket;
    }

    public override void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            // Set to typical parking ticket size (80mm x 60mm)
            page.Size(new PageSize(80, 60, Unit.Millimetre));
            page.Margin(4);
            page.Content().Column(column =>
            {
                column.Spacing(2); 
                
                // Combined Title and Hospital Header 
                column.Item().Row(row =>
                {
                    if (Ticket.Hospital?.HospitalLogo != null)
                    {
                        row.ConstantItem(35).Image(ConvertImageToBytes(Ticket.Hospital.HospitalLogo)).FitWidth();
                    }

                    row.RelativeItem().Column(c =>
                    {
                        // Moved the title here
                        c.Item().Text("PARKING TICKET")
                            .FontSize(8).Bold();
                            
                        c.Item().Text(Ticket.Hospital?.Name ?? "Hospital")
                            .FontSize(8).Bold();
                        c.Item().Text(Ticket.Hospital?.Address ?? "")
                            .FontSize(6).FontColor(Colors.Grey.Medium);
                    });
                });
                
                // Divider
                column.Item().Height(0.5f).Background(Colors.Grey.Lighten2);
                
                // Main Content Row
                column.Item().Row(mainRow =>
                {
                    // Left Column - Details (more compact)
                    mainRow.RelativeItem().Column(leftCol =>
                    {
                        leftCol.Item().Text($"Ticket: #{Ticket.TicketNumber}")
                                .FontSize(8).Bold(); 

                        leftCol.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(36); 
                                columns.RelativeColumn();
                            });


                            static IContainer CompactCellStyle(IContainer container)
                            {
                                return container.PaddingVertical(1).PaddingHorizontal(2);
                            }

                            table.Cell().Element(CompactCellStyle).Text("Entry:").FontSize(7);
                            table.Cell().Element(CompactCellStyle).Text($"{FormatDateTime(Ticket.CreationTime)}").FontSize(7);

                            if (Ticket.PaymentTime.HasValue)
                            {
                                table.Cell().Element(CompactCellStyle).Text("Paid:").FontSize(7);
                                table.Cell().Element(CompactCellStyle).Text($"{FormatDateTime(Ticket.PaymentTime.Value)}").FontSize(7);
                                // Calculate and show duration
                                TimeSpan duration = Ticket.PaymentTime.Value - Ticket.CreationTime;
                                table.Cell().Element(CompactCellStyle).Text("Duration:").FontSize(7);
                                table.Cell().Element(CompactCellStyle).Text($"{FormatDuration(duration)}").FontSize(7);
                            }

                            if (Ticket.DepartureTime.HasValue)
                            {
                                table.Cell().Element(CompactCellStyle).Text("Exit:").FontSize(7);
                                table.Cell().Element(CompactCellStyle).Text($"{FormatDateTime(Ticket.DepartureTime.Value)}").FontSize(7);
                            }
                            if (!Ticket.PaymentTime.HasValue)
                            {
                                table.Cell().Element(CompactCellStyle).Text("Status:").FontSize(7).Bold();
                                table.Cell().Element(CompactCellStyle).Text("VEHICLE IN PARKING").FontSize(7).Bold().FontColor(Colors.Green.Medium);
                            }
                        });

                        if (Ticket.TicketPayment != null)
                        {
                            leftCol.Item().PaddingTop(1).Table(table => // Reduced padding
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                static IContainer CompactCellStyle(IContainer container)
                                {
                                    return container.PaddingVertical(1).PaddingHorizontal(2);
                                }

                                table.Cell().Element(CompactCellStyle).Text("TOTAL:").FontSize(8).Bold();
                                table.Cell().Element(CompactCellStyle).Text($"{Ticket.TicketPayment.PaymentAmountTotal:C}").FontSize(8).Bold();

                                table.Cell().Element(CompactCellStyle).Text("Method:").FontSize(7);
                                table.Cell().Element(CompactCellStyle).Text(Ticket.TicketPayment.PaymentMethod).FontSize(7);
                            });
                        }
                    });

                    // Right Column - QR Code 
                    mainRow.ConstantItem(55).AlignRight().Column(qrCol => 
                    {
                        var qrCodeImage = GenerateQrCode(Ticket.TicketNumber, 145, 145);
                        if (qrCodeImage != null)
                        {
                            qrCol.Item().Width(55).Image(qrCodeImage);
                        }

                        if (Ticket.TicketPayment?.Subscription != null)
                        {
                            qrCol.Item().AlignCenter().Text($"Subscription")
                                  .FontSize(6).Bold();
                            qrCol.Item().AlignCenter().Text($"Card: {FormatCardNumber(Ticket.TicketPayment.Subscription.CardNumber)}")
                                  .FontSize(5).FontColor(Colors.Grey.Medium);
                            
                            // Show subscription validity
                            qrCol.Item().AlignCenter().Text($"Valid until: {Ticket.TicketPayment.Subscription.DateEnd.ToShortDateString()}")
                                  .FontSize(5).FontColor(Colors.Grey.Medium);
                        }
                    });
                });

                // Payment Warning
                if (Ticket.TicketPayment == null)
                {
                    column.Item().Background(Colors.Yellow.Lighten3).Padding(2)
                          .Text("Please pay at a payment station before leaving the parking with your vehicle.")
                          .FontSize(7).Italic().FontColor(Colors.Red.Medium).SemiBold();
                }
                else if (!Ticket.DepartureTime.HasValue)
                {
                    column.Item().Background(Colors.Yellow.Lighten3).Padding(2)
                          .Text("This ticket is only valid for 30 minutes, please head to the exit with your vehicle.")
                          .FontSize(7).Italic().FontColor(Colors.Red.Medium).SemiBold();
                }
                
                // Divider
                column.Item().Height(0.5f).Background(Colors.Grey.Lighten2);
                
                // Footer 
                column.Item().PaddingTop(0).Table(table =>
                {
                    table.ColumnsDefinition(columns => 
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });
                    
                    table.Cell().Text("Keep this ticket safe")
                        .FontSize(5).FontColor(Colors.Grey.Medium);
                    table.Cell().AlignRight().Text($"Hospital Parking {DateTime.Now.Year}")
                        .FontSize(5).FontColor(Colors.Grey.Medium);
                });
            });
        });
    }
    
    // Helper method to format date and time
    private string FormatDateTime(DateTime dateTime)
    {
        return $"{dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}";
    }
    
    // Helper method to format duration
    private string FormatDuration(TimeSpan duration)
    {
        if (duration.TotalDays >= 1)
            return $"{Math.Floor(duration.TotalDays)}d {duration.Hours}h {duration.Minutes}m";
        else if (duration.TotalHours >= 1)
            return $"{Math.Floor(duration.TotalHours)}h {duration.Minutes}m";
        else
            return $"{duration.Minutes}m";
    }
    
    // Helper method to mask card number for security
    private string FormatCardNumber(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 4)
            return cardNumber;
            
        return $"****{cardNumber.Substring(Math.Max(0, cardNumber.Length - 4))}";
    }
}
