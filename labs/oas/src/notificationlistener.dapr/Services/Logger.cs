using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace notificationlistener.dapr.Services
{
    public class Logger
    {

        TelemetryConfiguration telemetryConfiguration = TelemetryConfiguration.CreateDefault();

        public Logger(IConfiguration configuration)
        {
            telemetryConfiguration.InstrumentationKey = configuration["InstrumentationKey"];
        }

        public void LogMessage(string message)
        {
            var telemetryClient = new TelemetryClient(telemetryConfiguration);
            telemetryClient.TrackTrace(message);
            Console.WriteLine(message);

        }
    }
}

