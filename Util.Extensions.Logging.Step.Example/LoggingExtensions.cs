using Microsoft.Extensions.Logging;
using Serilog;

namespace Util.Extensions.Logging.Step.Example
{
    static class LoggingExtensions
    {
        private const string template = "{Timestamp:mm:ss}[{Level:u1}] {Scope} {Message:lj} {NewLine}";
        public static ILoggerFactory AddSerillogConsole(this ILoggerFactory lf)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: template)
                .CreateLogger();
            return lf.AddSerilog(logger);
        }
    }
}
