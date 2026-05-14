using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeterMonitor.Views;

namespace MeterMonitor.ViewModels;

public partial class LoginPageViewModel : PageViewModel<LoginPage>
{
    [ObservableProperty]
    public partial Uri? Website { get; private set; } = null;

    [ObservableProperty]
    public partial string UserInputWebsite { get; set; } = "";

    [ObservableProperty]
    public partial string? UserInputErrorInfo { get; private set; } = null;

    [ObservableProperty]
    public partial bool IsWebsiteDialogShow { get; private set; } = true;

    [ObservableProperty]
    public partial double WebsiteDialogOpacity { get; private set; } = 1;

    public LoginPageViewModel()
    {
        Title = "登录";
        SubmitCommand = StartGetInfoCommand;
    }

    [RelayCommand]
    private void StartGetInfo()
    {
    }

    [RelayCommand]
    private void ConfirmWebsiteInput()
    {
        Uri uri;
        try
        {
            uri = new Uri(UserInputWebsite);
        }
        catch (UriFormatException ex)
        {
            var msg = ex.Message;
            if (msg.StartsWith("Invalid URI")) msg = msg[13..];
            UserInputErrorInfo = $"无效的 URI。\n详细信息: {msg}";
            return;
        }
        Dispatcher.UIThread.Invoke(async () =>
        {
            WebsiteDialogOpacity = 0;
            await Task.Delay(TimeSpan.FromSeconds(.1));
            IsWebsiteDialogShow = false;
            Website = uri;
        });
    }

    // ReSharper disable once UnusedParameterInPartialMethod
    partial void OnUserInputWebsiteChanged(string value)
    {
        UserInputErrorInfo = null;
    }
}
