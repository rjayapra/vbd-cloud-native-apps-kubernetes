using Microsoft.AspNetCore.Mvc;
using Dapr;
using Dapr.Client;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using notificationlistener.dapr.Models;
using System;
using notificationlistener.dapr.Services;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace notificationlistener.dapr.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NotificationListenerController : ControllerBase
    {

        private readonly Logger _logger;
        private readonly KafkaHttpClient _client;

        public NotificationListenerController(KafkaHttpClient client, IConfiguration configuration, Logger logger)
        {
            _client = client;
            _logger = logger;
        }

        [Topic("pubsub", "notificationtopic")]
        [HttpPost("/receiver")]
        public async Task<IActionResult> Subscriber([FromBody] NotificationMessage message)
        {
            _client.SendEmailNotification(JsonSerializer.Serialize(message));
            _logger.LogMessage("Received Data:"+ message.Id);
            return Ok();
        }

    }  
     

}  