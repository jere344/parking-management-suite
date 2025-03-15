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
        { "Admin", 3 },
        { "Guest", 0 }
    };

    public static readonly Dictionary<string, string> HomeViews = new()
    {
        { "Admin", "Views/ViewAdmin.xaml" },
        { "Guest", "Views/ViewLogin.xaml" }
    };

    public static readonly Dictionary<string, DynamicViewInfoDictionary> ViewInfos = new()
    {
        {
            "Views/ViewLogin.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("login") },
                { "Icon", PackIconKind.Login},
                { "Permission", 0 },
                { "Hidden", true }
            }
        },
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
            "Views/ViewAdmin.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("admin") },
                { "Icon", PackIconKind.ShieldAccountOutline },
                { "Permission", 1 }
            }
        },
        {
            "Views/ViewProfile.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("profile") },
                { "Icon", PackIconKind.AccountDetailsOutline },
                { "Permission", 1 }
            }
        },
        {
            "Views/ViewHospitals.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("hospitals") },
                { "Icon", PackIconKind.Hospital },
                { "Permission", 1 }
            }
        },
        {
            "Views/ViewSubscriptionTiers.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("subscriptionTiers") },
                { "Icon", PackIconKind.CreditCardMultiple },
                { "Permission", 1 }
            }
        },
        {
            "Views/ViewDiscountCodes.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("discountCodes") },
                { "Icon", PackIconKind.BarcodeScan },
                { "Permission", 1 }
            }
        },
        {
            "Views/ViewTickets.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("tickets") },
                { "Icon", PackIconKind.TicketPercent },
                { "Permission", 1 }
            }
        },
        {
            "Views/ViewSubscription.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("subscriptions") },
                { "Icon", PackIconKind.CardAccountDetailsOutline },
                { "Permission", 1 }
            }
        },
        {
            "Views/PriceBrackets.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("priceBrackets") },
                { "Icon", PackIconKind.Tag },
                { "Permission", 1 }
            }
        },
        {
            "Views/Logout.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("logout") },
                { "Icon", PackIconKind.Logout },
                { "Permission", 1 } // permission 1 because guest doesn't need to logout
            }
        },
        {
            "Views/ViewGateControl.xaml",
            new()
            {
                { "Title", () => (string)Application.Current.FindResource("gateControl") },
                { "Icon", PackIconKind.Gate },
                { "Permission", 1 }
            }
        },
    };
}