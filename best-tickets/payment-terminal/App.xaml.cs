﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Threading;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using paymentterminal.Context;
using Microsoft.EntityFrameworkCore;
using ticketlibrary.Models;
using System.Net;

namespace paymentterminal;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    new public static App Current => (App)Application.Current;

    public User? ConnectedUser { 
        get => null;
        set { }
    }
  
    private BestTicketContext context = new();

    public System.Collections.IDictionary SavedSettings => (System.Collections.IDictionary?)Current.Properties["SavedSettings"] ?? throw new Exception("SavedSettings not found");

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        LoadSettingFile();
        // After loading the settings
        SetLanguageDictionary();
        SetTheme();
        SetPrimaryColor();

        // preload the database from there, so it won't be loaded after logging in causing a slow startup
        DataSeeder.Seed(context); // also seed the database if it's empty
    }


    public void SetLanguageDictionary()
    {
        ResourceDictionary dict = new ResourceDictionary();
        var language = (string)(SavedSettings["Language"] ?? Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
        switch (language)
        {
            case "en":
                dict.Source = new Uri(".\\Resources\\StringResources.en.xaml", UriKind.Relative);
                break;
            case "fr":
                dict.Source = new Uri(".\\Resources\\StringResources.fr.xaml", UriKind.Relative);
                break;
            default:
                dict.Source = new Uri(".\\Resources\\StringResources.en.xaml", UriKind.Relative);
                break;
        }
        this.Resources.MergedDictionaries.Add(dict);
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

    public void SetTheme()
    {
        string theme = (string)(SavedSettings["Theme"] ?? "light");
        ResourceDictionary dict = new ResourceDictionary();
        switch (theme)
        {
            case "dark":
                dict.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.dark.xaml");
                break;
            case "light":
                dict.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.light.xaml");
                break;
            default:
                dict.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.light.xaml");
                break;
        }
        this.Resources.MergedDictionaries.Add(dict);
    }

    public void SetPrimaryColor()
    {
        string theme = (string)(((App)Current).SavedSettings["Theme"] ?? "light");
        string color = (string)(((App)Current).SavedSettings["PrimaryColor"] ?? "Blue");

        ResourceDictionary dict = new ResourceDictionary();
        dict.Source = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.{color}.xaml");
        this.Resources.MergedDictionaries.Add(dict);

        // secondary is always lime
        var bundledTheme = new BundledTheme(){
            BaseTheme = theme == "dark" ? BaseTheme.Dark : BaseTheme.Light,
            PrimaryColor = (PrimaryColor)Enum.Parse(typeof(PrimaryColor), color),
            SecondaryColor = SecondaryColor.Lime
        };

        this.Resources.MergedDictionaries.Add(bundledTheme);
    }
}
