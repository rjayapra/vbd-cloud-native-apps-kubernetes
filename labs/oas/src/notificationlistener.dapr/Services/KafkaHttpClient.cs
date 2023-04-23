using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace notificationlistener.dapr.Services
{
    public class KafkaHttpClient
    {

        IResilientHttpClient _client;
        IConfiguration _configuration;
        Logger _logger;

        public KafkaHttpClient(IResilientHttpClient client, IConfiguration configuration, Logger logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        public void SendEmailNotification(string jsonObject)
        {
            _logger.LogMessage($"Inside SendEmailNotification  where jsonObject is {jsonObject}");
            _logger.LogMessage(_configuration["NotificationServiceURL"] + "/sendEmail");
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.Post<StringContent>(_configuration["NotificationServiceURL"] + "/sendEmail/", content);
            response.EnsureSuccessStatusCode();
            _logger.LogMessage($"Inside SendEmailNotification, updated table");
        }
    }
}