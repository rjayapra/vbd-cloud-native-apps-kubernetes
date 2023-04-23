using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace BidListener
{
    public class Logger
    {

        TelemetryConfiguration _telemetryConfiguration = TelemetryConfiguration.CreateDefault();
        TelemetryClient _telemetryClient;

        public Logger(IConfigurationRoot configuration, TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
            _telemetryClient.InstrumentationKey = configuration["InstrumentationKey"];
        }

        public void LogMessage(string message)
        {
            using (_telemetryClient.StartOperation<RequestTelemetry>("operation"))
            {
                _telemetryClient.TrackTrace(message);
                Console.WriteLine(message);
            }
        }
    }
}
