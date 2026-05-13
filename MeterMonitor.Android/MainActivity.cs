using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using ReactiveUI.Avalonia;

namespace MeterMonitor.Android;

[Activity(
    Label = "MeterMonitor.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity
{
}