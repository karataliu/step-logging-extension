using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;

namespace Util.Extensions.Logging.Step
{
    sealed class LoggingStep : ILoggingStep
    {
        private static readonly object[] emptyArgs = new object[0];
        private readonly ILogger logger;
        private readonly LogLevel level;
        private readonly string stepName;
        private readonly object[] args;
        private readonly IDisposable scope;
        private bool completed;
        private bool disposed;

        public LoggingStep(ILogger logger, LogLevel level, string stepName, object[] args)
        {
            this.logger = logger;
            this.level = level;
            this.stepName = stepName;
            this.args = args ?? emptyArgs;
            this.scope = logger.BeginStepScope(stepName);

            doLog(stepName);
        }

        public void Commit() => completed = true;

        private static void logWithLevel(ILogger logger, LogLevel level, string template, object[] args)
            => logger.Log(level, 0, new FormattedLogValues(template, args), null, (a, b) => a.ToString());

        private void doLog(string template) => logWithLevel(logger, level, template, args);

        void IDisposable.Dispose()
        {
            if (!disposed)
            {
                var completeMsg = completed ? "Done" : "Failed";
                doLog(stepName + " - " + completeMsg);
                scope.Dispose();

                disposed = true;
            }
        }
    }
}
