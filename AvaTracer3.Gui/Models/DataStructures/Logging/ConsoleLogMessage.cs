using Serilog.Events;

namespace AvaTracer3.Gui.Models.DataStructures.Logging;

public class ConsoleLogMessage
{
    public LogEventLevel LogLevel { get; set; }
    public string? Text { get; set; }
}