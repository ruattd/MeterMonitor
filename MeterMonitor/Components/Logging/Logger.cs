using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MeterMonitor.Components.Logging;

public static class Logger
{
    public static event Action<LogLevel, string>? LogEvent;

    private static readonly Channel<(LogLevel level, string message)> _channel = Channel.CreateUnbounded<(LogLevel, string)>();

    private static readonly CancellationTokenSource _loggerCts = new();

    private static async Task Consumer()
    {
        while (!_loggerCts.IsCancellationRequested)
        {
            var (level, message) = await _channel.Reader.ReadAsync(_loggerCts.Token).ConfigureAwait(false);
            LogEvent?.Invoke(level, message);
        }
    }

    static Logger()
    {
        Task.Run(Consumer);
    }

    public static async Task StopAsync()
    {
        await _loggerCts.CancelAsync().ConfigureAwait(false);
        _loggerCts.Dispose();
    }

    public static void Log(string message, LogLevel level = LogLevel.Debug)
    {
        _channel.Writer.TryWrite((level, message));
    }
}
