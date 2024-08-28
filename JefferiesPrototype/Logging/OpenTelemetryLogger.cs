using OpenTelemetry;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace JefferiesPrototype.Logging
{
    public class OpenTelemetryLogger
    {
        public OpenTelemetryLogger()
        {
            Sdk.CreateTracerProviderBuilder()
                .AddSource("MyOpenTelemetryProject")
                .AddConsoleExporter()
                .Build();
        }

        public void Log(string message)
        {
            var activitySource = new ActivitySource("MyOpenTelemetryProject");
            using (var activity = activitySource.StartActivity("LogMessage"))
            {
                activity?.AddTag("log.message", message);
            }
        }
    }
}
