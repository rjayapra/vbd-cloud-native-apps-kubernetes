using Confluent.Kafka;
using System;
using PaymentService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
namespace PaymentService.Services
{
    public class KafkaService
    {

        private string _paymentTopic;
        private ProducerConfig _config = null;
        private readonly IConfiguration _configuration;
        private readonly Logger _logger;
        public KafkaService(IConfiguration configuration, Logger logger)
        {

            _logger = logger;
            _configuration = configuration;
            _paymentTopic = _configuration["PaymentTopicName"];
        }

        public void SendMessage(AuctionPayment payment)
        {
            string topicName = "paymenttopic";
            var config = new ProducerConfig
            {
                BootstrapServers = _configuration["BootstrapServers"],
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SocketTimeoutMs = 60000,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "$ConnectionString",
                SaslPassword = _configuration["SaslPassword"],
                EnableSslCertificateVerification = false,
                BrokerVersionFallback = "1.0.0",
            };
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                var cancelled = false;
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cancelled = true;
                };

                try
                {
                    // Note: Awaiting the asynchronous produce request below prevents flow of execution
                    // from proceeding until the acknowledgement from the broker is received (at the 
                    // expense of low throughput).
                    string messageValue = JsonConvert.SerializeObject(payment);
                    string key = Guid.NewGuid().ToString();
                    string val = messageValue;
                    _logger.LogMessage("Value is " + val);
                    var deliveryReport = producer.ProduceAsync(
                        topicName, new Message<string, string> { Key = key, Value = val }).Result;

                    _logger.LogMessage($"delivered to: {deliveryReport.TopicPartitionOffset}");
                }
                catch (ProduceException<string, string> e)
                {
                    _logger.LogMessage($"failed to deliver message: {e.Message} [{e.Error.Code}]");
                }


                // Since we are producing synchronously, at this point there will be no messages
                // in-flight and no delivery reports waiting to be acknowledged, so there is no
                // need to call producer.Flush before disposing the producer.
            }
        }
    }
}