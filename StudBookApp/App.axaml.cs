﻿using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using StudBookApp.ViewModels;
using StudBookApp.Views;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

using System;
namespace StudBookApp;

using Avalonia.Markup.Xaml.Styling;
using StudBookApp.Themes.StyleHelpers;
using System;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static Styles FluentDark = new Styles
        {
            new StyleInclude(new Uri("avares://ControlCatalog/Styles"))
            {
                Source = new Uri("avares://Avalonia.Themes.Fluent/FluentDark.xaml")
            },
        };

    public static Styles FluentLight = new Styles
        {
            new StyleInclude(new Uri("avares://ControlCatalog/Styles"))
            {
                Source = new Uri("avares://Avalonia.Themes.Fluent/FluentLight.xaml")
            },
        };

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            //desktop.MainWindow = new MainWindow
            //{
            //    DataContext = new MainViewModel()
            //};

            var window = new MainWindow();
            window.DataContext = new MainViewModel(new StyleManager(window));
            desktop.MainWindow = window;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(null)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
