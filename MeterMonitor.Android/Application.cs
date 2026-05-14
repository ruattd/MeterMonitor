using Android.App;
using Android.Runtime;
using Android.Util;
using Avalonia;
using Avalonia.Android;
using MeterMonitor.Components.Logging;

namespace MeterMonitor.Android;

[Application]
public class Application : AvaloniaAndroidApplication<App>
{
    protected Application(nint javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
        Logger.LogEvent += (level, message) =>
        {
            var pri = level switch
            {
                LogLevel.Trace => LogPriority.Verbose,
                LogLevel.Debug => LogPriority.Debug,
                LogLevel.Info => LogPriority.Info,
                LogLevel.Warning => LogPriority.Warn,
                LogLevel.Error => LogPriority.Error,
                LogLevel.Fatal => LogPriority.Assert,
                _ => LogPriority.Info
            };
            Log.WriteLine(pri, null, message);
        };
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}
