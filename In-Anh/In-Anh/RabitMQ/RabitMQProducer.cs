using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace In_Anh.RabitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = "jinnie.shop"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            using (var connection = factory.CreateConnection())
            //Here we create channel with session and model
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("images", exclusive: false);
                //Serialize the message
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                //put the data on to the product queue
                channel.BasicPublish(exchange: "", routingKey: "images", body: body);

            }
                
            //declare the queue after mentioning name and a few property related to that
            
        }
    }
}
