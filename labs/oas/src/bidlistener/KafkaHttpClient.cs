using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BidListener
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

        public string GetBid()
        {
            HttpResponseMessage response = _client.Get(_configuration["AuctionServiceURL"]+"/auctions/");
            response.EnsureSuccessStatusCode();
            return response.Content.ToString();   
        }


        public void UpdateAuctionForBid(string jsonObject)
        {

            _logger.LogMessage($"Inside UpdateAuctionForBid where jsonObject is {jsonObject}");
            _logger.LogMessage(_configuration["AuctionServiceURL"] + "/updateAuctionForBid/");
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.Put<StringContent>(_configuration["AuctionServiceURL"]+"/updateAuctionForBid/", content);
            response.EnsureSuccessStatusCode();
            _logger.LogMessage($"Inside UpdateAuctionForBid, updated table");
        }
    }
}
