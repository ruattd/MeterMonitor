using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Avalonia.Android;

namespace MeterMonitor.Android;

[Activity(
    Label = "MeterMonitor.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity;
