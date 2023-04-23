using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationListener
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
            _logger = logger;
        }

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public void ConsumeMessages(string topic)
        {
            _logger.LogMessage("Running in " + _configuration["Mode"]);

            var config = new ConsumerConfig
            {
                BootstrapServers = _configuration["BootstrapServers"],
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SocketTimeoutMs = 60000,
                SessionTimeoutMs = 30000,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "$ConnectionString",
                SaslPassword = _configuration["SaslPassword"],
                GroupId = _configuration["GroupId"],
                EnableSslCertificateVerification = false,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                BrokerVersionFallback = "1.0.0",
            };
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {

                _logger.LogMessage("Subscribing topic");
                consumer.Subscribe(topic);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
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
                            _logger.LogMessage($"Got one message value is {cr.Value}");

                            if (IsValidJson(cr.Value))
                            {
                                _logger.LogMessage($"Value is {cr.Value}");
                                _client.SendEmailNotification(cr.Value);
                            }
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
