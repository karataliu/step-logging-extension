using System;

namespace Util.Extensions.Logging.Step
{
    public interface ILoggingStep : IDisposable
    {
        void Commit();
    }
}
