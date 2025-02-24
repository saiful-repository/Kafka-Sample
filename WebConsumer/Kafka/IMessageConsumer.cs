namespace WebConsumer.Kafka
{
    public interface IMessageConsumer
    {
        Task ReceivedMessage(CancellationToken stoppingToken);
    }
}
