using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MeterMonitor.ViewModels;

public abstract partial class PageViewModel : ViewModelBase
{
    public static RootLayoutViewModel RootLayout => RootLayoutViewModel.Instance;

    [ObservableProperty]
    public partial string Title { get; protected set; } = "Untitled";

    [ObservableProperty]
    public partial ICommand? SubmitCommand { get; protected set; } = null;
}
