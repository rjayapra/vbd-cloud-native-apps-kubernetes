using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BidListener 
{
    public class BidHostedService : IHostedService
    {
        private KafkaConsumer _consumer;
        private IConfigurationRoot _configuration;
        private Logger _logger;
        public BidHostedService(KafkaConsumer consumer, IConfigurationRoot configuration, Logger logger)
        {
            _consumer = consumer;
            _configuration = configuration;
            _logger = logger;  
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogMessage("Starting Listener for BidTopic");
            _consumer.ConsumeMessages(_configuration["BidTopicName"]);
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken) 
        {
            _logger.LogMessage("Stopped Listener for BidTopic");
            return Task.CompletedTask;
        }
    }
}
