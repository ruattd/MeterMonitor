using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MeterMonitor.ViewModels;
using MeterMonitor.Views;
using MeterMonitor.Components.Logging;

namespace MeterMonitor;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private static void LogException(string msg, LogLevel level = LogLevel.Warning) => Logger.Log(msg, level);

    public override void OnFrameworkInitializationCompleted()
    {
        // register exception log
        TaskScheduler.UnobservedTaskException += (_, e) =>
            LogException($"Exception thrown in background task.\n{e.Exception}");
        Dispatcher.UnhandledException += (_, e) =>
            LogException($"Exception thrown in dispatcher.\n{e.Exception}");
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            LogException($"Toplevel application exception thrown.\n{e.ExceptionObject}", LogLevel.Error);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new RootLayoutViewModel()
            };
        }
        else if (ApplicationLifetime is IActivityApplicationLifetime singleViewFactoryApplicationLifetime)
        {
            singleViewFactoryApplicationLifetime.MainViewFactory =
                () => new RootLayout { DataContext = new RootLayoutViewModel() };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new RootLayout
            {
                DataContext = new RootLayoutViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
