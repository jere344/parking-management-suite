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

public class SubscriptionCardDocument : DocumentBase
{
    public Subscription Subscription { get; }

    public SubscriptionCardDocument(Subscription subscription) : base()
    {
        Subscription = subscription;
    }

    public override void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            // Define a credit card-like size
            page.Size(new PageSize((float)85.6, (float)53.98, Unit.Millimetre));
            page.Margin(3);
            
            // Use a solid professional color instead of gradient
            page.Background("#1e3a8a"); // Deep blue color
            
            // Main content
            page.Content().Padding(3).Column(column =>
            {
                column.Spacing(5);

                // Header: Hospital Logo and Card Type
                column.Item().Row(row =>
                {
                    if (Subscription.Hospital?.HospitalLogo != null)
                    {
                        row.ConstantItem(30).Image(ConvertImageToBytes(Subscription.Hospital.HospitalLogo))
                            .FitHeight()
                            .FitWidth();
                    }
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text(Subscription.Hospital?.Name ?? "Hospital")
                            .FontSize(10)
                            .Bold()
                            .FontColor(Colors.White);
                            
                        col.Item().Text("PARKING SUBSCRIPTION")
                            .FontSize(12)
                            .Bold()
                            .FontColor(Colors.White)
                            .SemiBold();
                    });
                });
                
                // Add decorative element at the corner
                column.Item().AlignRight().Width(20).Height(3)
                    .Background(Colors.White.WithAlpha((byte)0.3f));
                
                // Card Body
                column.Item().Background(Colors.White.WithAlpha((byte)0.95f))
                    .Border(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Padding(5)
                    .Column(cardCol =>
                {
                    cardCol.Spacing(5);

                    // Card Number with styled display
                    cardCol.Item().Text(text =>
                    {
                        text.Span("CARD N° ").FontSize(8).Bold();
                        text.Span(FormatCardNumberWithSpaces(Subscription.CardNumber)).FontSize(12).Bold();
                    });
                    
                    // Two-column layout for card details
                    cardCol.Item().Row(row =>
                    {
                        // Left column
                        row.RelativeItem().Column(leftCol => 
                        {
                            leftCol.Item().Text("VALID FROM   -   UNTIL").FontSize(6).FontColor(Colors.Grey.Medium);
                            leftCol.Item().Text($"{Subscription.DateStart:dd/MM/yyyy} - {Subscription.DateEnd:dd/MM/yyyy}")
                                .FontSize(8)
                                .Bold();
                                
                            leftCol.Item().Height(3);
                            
                            leftCol.Item().Text("MAX USES PER DAY").FontSize(6).FontColor(Colors.Grey.Medium);
                            leftCol.Item().Text($"{Subscription.MaxNumberOfUsesPerDay}")
                                .FontSize(10)
                                .Bold();
                        });
                        
                        // Right column with QR code
                        row.ConstantItem(45).AlignRight().Column(qrCol => 
                        {
                            // Use appropriate size for QR code
                            var qrCodeImage = GenerateQrCode(Subscription.CardNumber, 150, 150);
                            if (qrCodeImage != null)
                            {
                                qrCol.Item().Width(45).Height(45).Image(qrCodeImage);
                            }
                        });
                    });
                    
                    // Payment information
                    cardCol.Item().BorderTop(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingTop(3).Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("SUBSCRIPTION PRICE").FontSize(6).FontColor(Colors.Grey.Medium);
                            col.Item().Text($"{Subscription.PricePaid:C}").FontSize(8).Bold();
                        });
                        
                        // Add a help text
                        row.RelativeItem().AlignRight().Text("Present at payment terminal")
                            .FontSize(7)
                            .FontColor(Colors.Grey.Medium)
                            .Italic();
                    });
                });
                
                // Footer with hospital address and terms
                column.Item().PaddingTop(2).Column(footerCol =>
                {
                    footerCol.Item().Text(Subscription.Hospital?.Address ?? "")
                        .FontSize(7)
                        .FontColor(Colors.White);
                        
                    footerCol.Item().Text("This card is non-transferable and remains property of the hospital.")
                        .FontSize(6)
                        .FontColor(Colors.White);
                });

                // Explaination for the user
                column.Item().PaddingTop(6).Text("This card is valid for parking at the hospital parking only.\nUse it to fund your parking ticket at the payment terminal.")
                    .FontSize(6)
                    .FontColor(Colors.White);

            });
        });
    }
    
    // Helper method to format card number with spaces for better readability
    private string FormatCardNumberWithSpaces(string cardNumber)
    {
        return cardNumber.Replace("-", " - ");
    }
}