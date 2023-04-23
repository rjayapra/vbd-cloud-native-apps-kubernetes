using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace PaymentService.Services
{
    public class Logger
    {
        TelemetryClient _telemetryClient;
        public Logger(IConfiguration configuration, TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
            _telemetryClient.InstrumentationKey = configuration["InstrumentationKey"];
        }

        public void LogMessage(string message)
        {
            _telemetryClient.TrackTrace(message);
            Console.WriteLine(message);

        }
    }
}
