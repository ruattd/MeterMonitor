using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MeterMonitor.ViewModels;

public abstract partial class PageViewModel : ViewModelBase
{
    public static RootLayoutViewModel RootLayout => RootLayoutViewModel.Instance;

    [ObservableProperty]
    public partial string Title { get; protected set; } = "Untitled";

    [ObservableProperty]
    public partial ICommand? SubmitCommand { get; protected set; } = null;

    public abstract UserControl Content { get; }
}

public abstract class PageViewModel<TPageView> : PageViewModel
    where TPageView : UserControl, new()
{
    public override UserControl Content { get; }

    protected PageViewModel()
    {
        Content = new TPageView { DataContext = this };
    }
}
