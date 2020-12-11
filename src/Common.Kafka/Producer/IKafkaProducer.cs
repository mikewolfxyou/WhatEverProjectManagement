namespace Common.Kafka.Producer
{
    public interface IKafkaProducer<in TKey, in TValue>
    {
        void Publish(TKey key, TValue value);
    }
}