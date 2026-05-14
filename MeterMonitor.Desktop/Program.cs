using System;
using System.Diagnostics;
using Avalonia;
using MeterMonitor.Components.Logging;

namespace MeterMonitor.Desktop;

internal static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        Logger.LogEvent += (level, message) =>
        {
            var msg = $"[{level}] {message}";
            Console.Write(DateTime.Now.ToString("[HH:mm:ss] "));
            Console.WriteLine(msg);
            Debugger.Log((int)level, null, msg);
        };
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
