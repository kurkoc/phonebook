namespace Report.API.RabbitMq
{
    public interface IRabbitMqProducer<T>
    {
        void Publish(T @event);
    }
}
