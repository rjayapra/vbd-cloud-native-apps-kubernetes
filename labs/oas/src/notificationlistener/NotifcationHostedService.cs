using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationListener
{
    public class NotificationHostedService : IHostedService
    {
        private KafkaConsumer _consumer;
        private IConfigurationRoot _configuration;
        private Logger _logger;
        public NotificationHostedService(KafkaConsumer consumer, IConfigurationRoot configuration, Logger logger)
        {
            _consumer = consumer;
            _configuration = configuration;
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogMessage("Starting Listener for NotificationTopic");
            _consumer.ConsumeMessages(_configuration["NotificationTopicName"]);
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogMessage("Stopped Listener for NotificationTopic");
            return Task.CompletedTask;
        }
    }
}
