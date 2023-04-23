using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BidListener
{

    public class KafkaConsumer
    {

        private KafkaHttpClient _client;
        private IConfigurationRoot _configuration;
        private Logger _logger;

        public KafkaConsumer(KafkaHttpClient client, IConfigurationRoot configuration, Logger logger)
        {  
            _client = client;
            _configuration = configuration;
            _logger= logger;
        }


        public void ConsumeMessages(string topic)
        {
            // _client.GetBid();


            _logger.LogMessage("Running in " + _configuration["Mode"]);

            var config = new ConsumerConfig
            {
                
                BootstrapServers = _configuration["BootstrapServers"],  //"onlineauctionkafkahub.servicebus.windows.net:9093",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SocketTimeoutMs = 60000,                //this corresponds to the Consumer config `request.timeout.ms`
                SessionTimeoutMs = 30000,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "$ConnectionString",
                SaslPassword = _configuration["SaslPassword"],  
                GroupId = _configuration["GroupId"], 
                EnableSslCertificateVerification=false,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                BrokerVersionFallback = "1.0.0",       
            };
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
               
                _logger.LogMessage("Subscribing topic");
                consumer.Subscribe(topic);
                
                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };
                try
                {

                    _logger.LogMessage("Starting loop to check for Kafka messages");
                    while (true)
                    {
                        _logger.LogMessage("Inside While loop to check for Kafka messages");
                        try
                        {
                           var cr = consumer.Consume(cts.Token);

                      
                 
                           // string cr = "{ \"bidid\":\"df936256-6816-4fe8-b54e-3a411070bcb2\",\"auctionId\":\"136\", \"bidAmount\":\"316\",\"userId\":\"100\",\"userName\":\"oasuser\",\"bidDate\":1632997937187}";

                            _logger.LogMessage($"Got one message value is {cr.Value}");
                            dynamic data = JObject.Parse(cr.Value);
                            //dynamic data = JObject.Parse(cr);
                            string bidId = data.bidid;
                            string customerId = data.userId;
                            decimal bidAmount = Convert.ToDecimal(data.bidAmount);
                            int auctionId = Convert.ToInt32(data.auctionId);

                            _logger.LogMessage($"Calling Update Auction for Bid for {cr.Value}");
                            _client.UpdateAuctionForBid(cr.Value);
                            //_client.UpdateAuctionForBid(cr);

                            _logger.LogMessage($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");

                       
                        }
                        catch (ConsumeException e)
                        {
                            _logger.LogMessage($"Error occured: {e.Message} Stack Trace: {e.StackTrace}");
                       
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    consumer.Close();
                }
            }

        }

    }
}
