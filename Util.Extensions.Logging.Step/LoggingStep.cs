using System;
using Microsoft.Extensions.Logging;

namespace Util.Extensions.Logging.Step
{
    sealed class LoggingStep : ILoggingStep
    {
        private static readonly object[] emptyArgs = new object[0];
        private readonly ILogger logger;
        private readonly LogLevel level;
        private readonly string stepName;
        private readonly object[] args;
        private bool completed;
        private bool disposed;

        public LoggingStep(ILogger logger, LogLevel level, string stepName, object[] args)
        {
            this.logger = logger;
            this.level = level;
            this.stepName = stepName;
            this.args = args ?? emptyArgs;

            doLog(stepName);
        }

        public void Commit() => completed = true;

        private void doLog(string template) => logger.Log(level, template, args);

        void IDisposable.Dispose()
        {
            if (!disposed)
            {
                var completeMsg = completed ? "Done" : "Failed";
                doLog(stepName + " - " + completeMsg);

                disposed = true;
            }
        }
    }
}
