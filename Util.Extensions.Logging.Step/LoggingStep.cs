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

            logWithLevel(level, stepName);
        }

        public void Commit() => completed = true;

        private void logWithLevel(LogLevel level, string template)
            => logger.Log(level, 0, new FormattedLogValues(template, args), null, (a, b) => a.ToString());

        void IDisposable.Dispose()
        {
            if (!disposed)
            {
                (var completeMsg, var l1) = completed ? ("Done", level) : ("Failed", LogLevel.Error);
                logWithLevel(l1, stepName + " - " + completeMsg);
                scope.Dispose();

                disposed = true;
            }
        }
    }
}
