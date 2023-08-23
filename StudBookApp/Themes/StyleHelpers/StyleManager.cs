using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Styling;

using System;

namespace StudBookApp.Themes.StyleHelpers
{
    public sealed class StyleManager
    {
        private readonly StyleInclude _defaultStyle = CreateStyle("avares://StudBookApp/Themes/Default.axaml");
        private readonly StyleInclude _newStyle = CreateStyle("avares://StudBookApp/Themes/Khai.axaml");
        private readonly Window _window;

        public StyleManager(Window window)
        {
            _window = window;

            // We add the style to the window styles section, so it
            // will override the default style defined in App.xaml. 
            if (window.Styles.Count == 0)
                window.Styles.Add(_defaultStyle);

            // If there are styles defined already, we assume that
            // the first style imported it related to citrus.
            // This allows one to override citrus styles.
            else window.Styles[0] = _defaultStyle;
        }

        public Theme CurrentTheme { get; private set; } = Theme.Default;

        public void UseTheme(Theme theme)
        {
            // Here, we change the first style in the main window styles
            // section, and the main window instantly refreshes. Remember
            // to invoke such methods from the UI thread.
            _window.Styles[0] = theme switch
            {
                Theme.Default => _defaultStyle,
                Theme.Khai => _newStyle,
                _ => throw new ArgumentOutOfRangeException(nameof(theme))
            };

            CurrentTheme = theme;
        }

        public void UseNextTheme()
        {
            // This method allows to support switching among all
            // supported color schemes one by one.
            UseTheme(CurrentTheme switch
            {
                Theme.Default => Theme.Khai,
                Theme.Khai => Theme.Default,
                _ => throw new ArgumentOutOfRangeException(nameof(CurrentTheme))
            });
        }

        private static StyleInclude CreateStyle(string url)
        {
            //return new StyleInclude(new Uri("avares://StudBookApp/App.xaml"))
            //{
            //    Source = new Uri(url)
            //};

            return new StyleInclude(new Uri("resm:Themes?assembly=StudBookApp"))
            {
                Source = new Uri(url)
            };
        }
    }
}
