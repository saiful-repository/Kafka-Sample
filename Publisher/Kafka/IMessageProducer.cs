namespace Publisher.Kafka
{
    public interface IMessageProducer
    {
        Task SendMessage<T> (T message);
    }
}
