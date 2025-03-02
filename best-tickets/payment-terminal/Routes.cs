using System.Windows;
using MaterialDesignThemes.Wpf;




public class DynamicViewInfoDictionary : Dictionary<string, object>
{
    public new object? this[string key]
    {
        get
        {
            if (base.TryGetValue(key, out var value))
            {
                // Check if the value is a Func<string> and invoke it if so
                if (value is Func<string> func)
                {
                    return func();
                }
                return value;
            }
            return null;
        }
    }
}


public static class Routes
{

    public static readonly Dictionary<string, int> PermissionsLevels = new()
    {
        { "Guest", 0 }
    };

    public static readonly Dictionary<string, string> HomeViews = new()
    {
        { "Guest", "Views/ViewHome.xaml" }
    };

    public static readonly Dictionary<string, DynamicViewInfoDictionary> ViewInfos = new()
    {
        {
            "Views/ViewSettings.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("settings") },
                { "Icon", PackIconKind.CogOutline },
                { "Permission", 0 }
            }
        },
        {
            "Views/ViewHome.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("home") },
                { "Icon", PackIconKind.HomeOutline },
                { "Permission", 0 }
            }
        },
        {
            "Views/ViewSingleTicket.xaml",
            new()
            {
                { "Title", () => "Ticket Unique" },
                { "Icon", PackIconKind.TicketOutline },
                { "Permission", 0 }
            }
        },
        {
            "Views/ViewSingleTicketPayment.xaml",
            new()
            {
                { "Title", () => "Paiement Ticket" },
                { "Icon", PackIconKind.CreditCardOutline },
                { "Permission", 0 }
            }
        },
        {
            "Views/ViewTicketPrint.xaml",
            new()
            {
                { "Title", () => "Impression du Ticket" },
                { "Icon", PackIconKind.Printer },
                { "Permission", 0 }
            }
        },

    };
}