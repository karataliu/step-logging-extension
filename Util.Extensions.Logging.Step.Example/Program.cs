using Microsoft.Extensions.Logging;

namespace Util.Extensions.Logging.Step.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var lf = createLoggerFactory())
            {
                var logger = lf.CreateLogger<Program>();
                logger.LogInformation("Begin");
                basicDemo(logger);
            }
        }

        private static void basicDemo(ILogger logger)
        {
            // Comitted step
            using (var s = logger.StepInformation("step1"))
            {
                s.Commit();
            }

            // Uncommitted step
            using (var s = logger.StepInformation("step2"))
            {
            }

            // Nested step
            using (var s = logger.StepInformation("step1"))
            {
                using (var s1 = logger.StepInformation("step1.1"))
                {
                    s.Commit();
                }
                using (var s1 = logger.StepInformation("step1.2"))
                {
                }
            }
        }

        private static ILoggerFactory createLoggerFactory() => new LoggerFactory().AddSerillogConsole();
    }
}
