using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Util.Extensions.Logging.Step
{
    public static class ScopeExtensions
    {
        public static IDisposable BeginKeyValueScope(this ILogger logger, string key, string value)
            => logger.BeginScope(new KeyValuePair<string, string>(key, value));
    }
}
