using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionPaymentListener
{
    public class AuctionPaymentHostedService : IHostedService
    {
        private KafkaService _service;
        private IConfigurationRoot _configuration;
        private Logger _logger;
        public AuctionPaymentHostedService(KafkaService service, IConfigurationRoot configuration, Logger logger)
        {
            _service = service;
            _configuration = configuration;
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogMessage("Starting Listener for PaymentTopic");
            _service.ConsumeMessages(_configuration["PaymentTopicName"]);
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogMessage("Stopped Listener for PaymentTopic");
            return Task.CompletedTask;
        }
    }
}
