
using Confluent.Kafka;
using Newtonsoft.Json;
using WebConsumer.Models;
using Serilog;

namespace WebConsumer.Kafka
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly IConsumer<string, string> _consumer;

        public MessageConsumer(IConsumer<string, string> consumer)
        {
            _consumer = consumer;
        }

        public async Task ReceivedMessage(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("message_topic");

            try
            {
                await Task.Run(() =>
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var response = _consumer.Consume(stoppingToken);

                        if (response?.Message != null)
                        {
                            var message = JsonConvert.DeserializeObject<Message>(response.Message.Value);
                            Log.Information($"{{message: {message.Text}, sender: {message.Sender}}}");
                        }
                    }
                }, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                Log.Information("Kafka Consumer shutting down...");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error consuming Kafka message");
            }
            finally
            {
                _consumer.Close();
                _consumer.Dispose();
            }            
        }
    }
}
