
using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Publisher.Kafka
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IProducer<string, string> _producer;

        public MessageProducer(IProducer<string, string> producer)
        {
            _producer = producer;
        }

        public async Task SendMessage<T>(T message)
        {           
            // Serialize the message object to a JSON string.
            var json = JsonConvert.SerializeObject(message);

            // Asynchronously sends a message to the Kafka topic named "message_topic".
            await _producer.ProduceAsync("message_topic", new Message<string, string> { Key = Guid.NewGuid().ToString(),  Value = json });
        }
    }
}
