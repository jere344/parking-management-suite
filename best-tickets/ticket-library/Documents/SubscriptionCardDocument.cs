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
            // Define a card-like size.
            page.Size(new PageSize(90, 55, Unit.Millimetre));
            page.Margin(5);
            page.Background(Colors.White);

            page.Content().Column(column =>
            {
                column.Spacing(5);

                // Header: Hospital Logo and Details.
                column.Item().Row(row =>
                {
                    if (Subscription.Hospital?.HospitalLogo != null)
                    {
                        var logoBytes = ConvertImageToBytes(Subscription.Hospital.HospitalLogo);
                        row.ConstantItem(30).Image(logoBytes).FitHeight();
                    }
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text(Subscription.Hospital?.Name ?? "Hospital")
                            .FontSize(12)
                            .Bold();
                        col.Item().Text(Subscription.Hospital?.Address ?? "")
                            .FontSize(8)
                            .FontColor(Colors.Grey.Medium);
                    });
                });

                // Card Information Section.
                column.Item().Border(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Padding(5)
                    .Column(cardCol =>
                {
                    cardCol.Spacing(3);

                    // Title
                    cardCol.Item().Text("Carte de Souscription")
                        .FontSize(14)
                        .Bold()
                        .AlignCenter();

                    // Subscription Card Number
                    cardCol.Item().Text($"N° {Subscription.CardNumber}")
                        .FontSize(12)
                        .Bold()
                        .AlignCenter();

                    // Validity Dates
                    cardCol.Item().Row(detailsRow =>
                    {
                        detailsRow.RelativeItem().Text($"Début: {Subscription.DateStart:dd/MM/yyyy}")
                            .FontSize(10);
                        detailsRow.RelativeItem().Text($"Fin: {Subscription.DateEnd:dd/MM/yyyy}")
                            .FontSize(10)
                            .AlignRight();
                    });

                    // Maximum number of uses per day.
                    cardCol.Item().Text($"Utilisations max/jour: {Subscription.MaxNumberOfUsesPerDay}")
                        .FontSize(10)
                        .AlignCenter();
                });

                // QR Code Section.
                column.Item().AlignCenter().Element(container =>
                {
                    // Use 100x100 for QR code generation here.
                    var qrCodeImage = GenerateQrCode(Subscription.CardNumber, 100, 100);
                    if (qrCodeImage != null)
                    {
                        container.Width(50).Height(50).Image(qrCodeImage);
                    }
                    return container;
                });
            });
        });
    }
}