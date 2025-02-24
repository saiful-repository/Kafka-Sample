
using Serilog;

namespace WebConsumer.Kafka
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer;
        public KafkaConsumerService(IMessageConsumer messageConsumer)
        {
            _messageConsumer = messageConsumer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Background Service Run");

            await _messageConsumer.ReceivedMessage(stoppingToken);
        }
    }
}
