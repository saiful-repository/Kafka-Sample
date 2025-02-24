using Microsoft.AspNetCore.Mvc;
using Publisher.Kafka;
using Publisher.Models;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;
        public MessageController(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage(Message message)
        {
            // Send the message to Kafka topic
            await _messageProducer.SendMessage(message);

            return Ok("Message has been sent to Kafka topic.");
        }
    }
}
