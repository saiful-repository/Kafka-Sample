using Confluent.Kafka;
using Newtonsoft.Json;
using ConsoleConsumer.Models;

var config = new ConsumerConfig()
{
    BootstrapServers = "localhost:9092",
    GroupId = "message_group_id",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<string, string>(config).Build();

consumer.Subscribe("message_topic");

CancellationTokenSource token = new CancellationTokenSource();

try
{
    while (true)
    {
        var response = consumer.Consume(token.Token);

        if (response.Message != null)
        {
            var message = JsonConvert.DeserializeObject<Message>(response.Message.Value);
            Console.WriteLine($"{{message: {message.Text}, sender: {message.Sender}}}");
        }
    }
}
catch(Exception ex)
{
    consumer.Dispose();
    throw;
}


