using System.Configuration;
using System.Data;
using System.Windows;
using DotNetEnv;

namespace GatesSoftware;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public System.Collections.IDictionary SavedSettings => (System.Collections.IDictionary?)Current.Properties["SavedSettings"] ?? throw new Exception("SavedSettings not found");

    protected override void OnStartup(StartupEventArgs e)
    {
        Env.Load();
        LoadSettingFile();
        base.OnStartup(e);
        
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        SaveSettings();
    }

    public static void SaveSettings()
    {
        // we save ((App)Current).SavedSettings in settings.json
        // we can load it at startup
        // With this method, we can access the settings from any class
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(Application.Current.Properties["SavedSettings"]);
        System.IO.File.WriteAllText("settings.json", json);
    }

    public static void LoadSettingFile()
    {
        // create a "SavedSettings" Dictionary in ((App)Current).SavedSettings
        Current.Properties["SavedSettings"] = new Dictionary<string, object>();

        // we load ((App)Current).SavedSettings from settings.json
        // we can load it at startup
        if (System.IO.File.Exists("settings.json"))
        {
            string json = System.IO.File.ReadAllText("settings.json");
            var values = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.IDictionary>(json);
            if (values == null)
            {
                return;
            }
            foreach (var key in values.Keys)
            {
                var str_key = key.ToString();
                if (str_key == null)
                {
                    continue;
                }
                ((App)Current).SavedSettings[str_key] = values[key];
            }
        }
    }
}

