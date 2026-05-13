using CommunityToolkit.Mvvm.ComponentModel;

namespace MeterMonitor.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ViewModelBase CurrentPage { get; set; } = new MainPageViewModel();
}
