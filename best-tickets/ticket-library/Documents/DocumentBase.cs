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

// Base class providing common functionality.
public abstract class DocumentBase : IDocument
{
    protected DocumentBase()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    // Each subclass must provide its own Compose implementation.
    public abstract void Compose(IDocumentContainer container);

    // Converts a BitmapImage to a byte array.
    protected byte[]? ConvertImageToBytes(BitmapImage image)
    {
        using (var memoryStream = new MemoryStream())
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(memoryStream);
            return memoryStream.ToArray();
        }
    }

    // Generates a QR code image as a PNG byte array for the given content.
    // The width and height are passed in to allow for different document requirements.
    protected byte[]? GenerateQrCode(string content, int width, int height)
    {
        var writer = new ZXing.BarcodeWriterPixelData
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
                Height = height,
                Width = width,
                Margin = 1
            }
        };

        var pixelData = writer.Write(content);
        using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height,
                    System.Drawing.Imaging.PixelFormat.Format32bppRgb))
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
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
