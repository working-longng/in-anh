using In_Anh.Controllers;

namespace In_Anh.RabitMQ
{
    public interface IRabitMQProducer 
    {
        public void SendProductMessage<T>(T message);
    }
}
