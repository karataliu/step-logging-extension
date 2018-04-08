using Microsoft.Extensions.Logging;

namespace Util.Extensions.Logging.Step
{
    public static class LoggingStepExtensions
    {
        public static ILoggingStep StepInformation(this ILogger logger, string stepName, params object[] args)
            => logger.StepLevel(LogLevel.Information, stepName, args);

        public static ILoggingStep StepDebug(this ILogger logger, string stepName, params object[] args)
            => logger.StepLevel(LogLevel.Debug, stepName, args);

        private static ILoggingStep StepLevel(
            this ILogger logger,
            LogLevel level,
            string stepName,
            object[] args)
            => new LoggingStep(logger, level, stepName, args);
    }
}
