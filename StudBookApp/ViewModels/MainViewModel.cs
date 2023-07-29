using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using ReactiveUI;
using StudBookApp.ViewModels.Base;
using System;
using System.Reactive;
using StudBookApp.Themes.StyleHelpers;

namespace StudBookApp.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Properties



    #endregion

    #region Commands

    public ReactiveCommand<Unit, Unit> CloseApplicationCommand { get; }
    void CloseApplication()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }

    public ReactiveCommand<Unit, Unit> ChangeThemeCommand { get; }
    void ChangeTheme(StyleManager? styles)
    {
        styles?.UseTheme(styles.CurrentTheme switch
        {
            Theme.Default => Theme.New,
            Theme.New => Theme.Default,
            _ => throw new ArgumentOutOfRangeException(nameof(styles.CurrentTheme))
        });
    }

    #endregion

    public MainViewModel(StyleManager? styles)
    {
        CloseApplicationCommand = ReactiveCommand.Create(CloseApplication);

        ChangeThemeCommand = ReactiveCommand.Create(() => ChangeTheme(styles));
    }
}
