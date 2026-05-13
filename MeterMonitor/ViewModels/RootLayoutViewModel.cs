using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MeterMonitor.ViewModels;

public partial class RootLayoutViewModel : ViewModelBase
{
    public static RootLayoutViewModel Instance { get; } = new();

    [ObservableProperty]
    public partial PageViewModel CurrentPage { get; private set; } = new MainPageViewModel();

    [ObservableProperty]
    public partial bool CanBack { get; private set; } = false;

    [ObservableProperty]
    public partial double TitleBarTranslateX { get; private set; } = 0;

    [ObservableProperty]
    public partial double TitleBarOpacity { get; private set; } = 1;

    private readonly Stack<PageViewModel> _navigationStack = new();

    public void Navigate(PageViewModel? target, bool clearStack = false)
    {
        double translateX;
        if (target == null)
        {
            if (!_navigationStack.TryPop(out target)) return;
            translateX = -40;
        }
        else
        {
            if (clearStack) _navigationStack.Clear();
            else _navigationStack.Push(CurrentPage);
            translateX = 40;
        }
        Dispatcher.UIThread.Invoke(async () =>
        {
            TitleBarTranslateX = translateX;
            TitleBarOpacity = 0;
            await Task.Delay(TimeSpan.FromSeconds(.1));
            CurrentPage = target;
            CanBack = _navigationStack.Count > 0;
            TitleBarTranslateX = -translateX;
            await Task.Delay(TimeSpan.FromSeconds(.1));
            TitleBarOpacity = 1;
            TitleBarTranslateX = 0;
        });
    }

    public ICommand GoBack { get; }

    public ICommand GoForward { get; }

    public ICommand SetCurrent { get; }

    public RootLayoutViewModel()
    {
        GoBack = new RelayCommand(() => Navigate(null));
        GoForward = new RelayCommand<PageViewModel>(target => Navigate(target));
        SetCurrent = new RelayCommand<PageViewModel>(target => Navigate(target, true));
    }
}
