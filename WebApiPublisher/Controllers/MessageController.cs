using MessageContract;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;

namespace WebApiPublisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public MessageController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("sendmessage", Name = "sendmessage")]
        public async Task<IActionResult> SendMessageAsync([FromBody] string title)
        {
            try
            {
                var message = new TopicMessage(title);
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
                ISubscriber sub = redis.GetSubscriber();

                string messageJson = JsonSerializer.Serialize(message);

                await sub.PublishAsync(message.Topic, messageJson);

                return Ok(message);
            }
            catch (Exception ex) { 
                return BadRequest(ex);
            }
        }
    }
}
