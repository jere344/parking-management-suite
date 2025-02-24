namespace ticket_library;

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

public class TicketDocument : IDocument
{
    public Ticket Ticket { get; }

    public TicketDocument(Ticket ticket)
    {
        Ticket = ticket;
        QuestPDF.Settings.License = LicenseType.Community; 
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
{
    container.Page(page =>
    {
        // Set to typical parking ticket size (80mm x 200mm)
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
                    var qrCodeImage = GenerateQrCode(Ticket.TicketNumber);
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

    private byte[]? ConvertImageToBytes(BitmapImage image)
    {
        using (var memoryStream = new MemoryStream())
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(memoryStream);
            return memoryStream.ToArray();
        }
    }

    // Generates a QR code image (as a byte array in PNG format) for the given content.
    private byte[]? GenerateQrCode(string content)
    {
        var writer = new ZXing.BarcodeWriterPixelData
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
                Height = 150,
                Width = 150,
                Margin = 1
            }
        };

        var pixelData = writer.Write(content);
        using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height,
                    System.Drawing.Imaging.PixelFormat.Format32bppRgb))
        {
            var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            try
            {
                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
