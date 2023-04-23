using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NotificationListener
{
    public class KafkaHttpClient
    {

        IResilientHttpClient _client;
        IConfigurationRoot _configuration;
        Logger _logger;

        public KafkaHttpClient(IResilientHttpClient client, IConfigurationRoot configuration, Logger logger)
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