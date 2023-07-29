using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using StudBookApp.Views.CustomTitleBars;

namespace StudBookApp.Views;

public partial class MainWindow : Window
{
    private const string PartTitleBar = "TitleBar";
    private WindowsTitleBar? _titleBar;

    public MainWindow()
    {
        InitializeComponent();
        _titleBar = this.FindNameScope().Find<WindowsTitleBar>(PartTitleBar);
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (Equals((e.Source as ContentPresenter)?.Content, _titleBar?.Content))
        {
            BeginMoveDrag(e);
        }
    }
}
