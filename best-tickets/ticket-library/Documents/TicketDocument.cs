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
            page.Margin(5);
            page.Content().Column(column =>
            {
                column.Spacing(4);

                // Hospital Header
                column.Item().Row(row =>
                {
                    if (Ticket.Hospital?.HospitalLogo != null)
                    {
                        var logoBytes = ConvertImageToBytes(Ticket.Hospital.HospitalLogo);
                        row.ConstantItem(40).Image(logoBytes).FitWidth();
                    }

                    row.RelativeItem().PaddingVertical(2).Column(c =>
                    {
                        c.Item().Text(Ticket.Hospital?.Name ?? "Hospital")
                            .FontSize(10).Bold();
                        c.Item().Text(Ticket.Hospital?.Address ?? "")
                            .FontSize(6).FontColor(Colors.Grey.Medium);
                    });
                });

                // Main Content Row
                column.Item().Row(mainRow =>
                {
                    // Left Column - Details
                    mainRow.RelativeItem().Column(leftCol =>
                    {
                        leftCol.Item().Text($"#{Ticket.TicketNumber}")
                                .FontSize(12).Bold();

                        leftCol.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);
                                columns.RelativeColumn();
                            });

                            table.Cell().Element(CellStyle).Text("Created:").FontSize(7);
                            table.Cell().Element(CellStyle).Text($"{Ticket.CreationTime}").FontSize(7);

                            if (Ticket.PaymentTime.HasValue)
                            {
                                table.Cell().Element(CellStyle).Text("Paid:").FontSize(7);
                                table.Cell().Element(CellStyle).Text($"{Ticket.PaymentTime.Value}").FontSize(7);
                            }

                            if (Ticket.DepartureTime.HasValue)
                            {
                                table.Cell().Element(CellStyle).Text("Departure:").FontSize(7);
                                table.Cell().Element(CellStyle).Text($"{Ticket.DepartureTime.Value}").FontSize(7);
                            }
                        });

                        // Local function for table cell styling.
                        static IContainer CellStyle(IContainer container)
                        {
                            return container.PaddingVertical(2).PaddingHorizontal(4);
                        }

                        if (Ticket.TicketPayment != null)
                        {
                            leftCol.Item().PaddingTop(4).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell().Element(CellStyle).Text("TOTAL:").FontSize(8).Bold();
                                table.Cell().Element(CellStyle).Text($"{Ticket.TicketPayment.PaymentAmountTotal:C}").FontSize(8).Bold();

                                table.Cell().Element(CellStyle).Text("Method:").FontSize(6);
                                table.Cell().Element(CellStyle).Text(Ticket.TicketPayment.PaymentMethod).FontSize(6);
                            });
                        }
                    });

                    // Right Column - QR Code
                    mainRow.ConstantItem(60).AlignRight().Column(qrCol =>
                    {
                        // Use 150x150 for QR code generation in TicketDocument.
                        var qrCodeImage = GenerateQrCode(Ticket.TicketNumber, 150, 150);
                        if (qrCodeImage != null)
                        {
                            qrCol.Item().Width(60).Image(qrCodeImage);
                        }

                        if (Ticket.TicketPayment?.Subscription != null)
                        {
                            qrCol.Item().PaddingTop(2).Text($"Sub: {Ticket.TicketPayment.Subscription.CardNumber}")
                                    .FontSize(5).FontColor(Colors.Grey.Medium);
                        }
                    });
                });

                // Payment Warning (if needed)
                if (Ticket.TicketPayment == null)
                {
                    column.Item().PaddingTop(4).Text("Payment required at pay station")
                            .FontSize(8).Italic().FontColor(Colors.Red.Medium);
                }
            });
        });
    }
}
