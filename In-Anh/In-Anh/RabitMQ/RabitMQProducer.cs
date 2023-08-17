using Lucene.Net.Support;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace In_Anh.RabitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {

            try
            { //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
                ConnectionFactory factory = new ConnectionFactory();
                factory.Uri = new Uri("amqp://admin:admin@jinnie.shop/%2f");

                ConnectionFactory.DefaultAddressFamily = AddressFamily.InterNetwork;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //Create the RabbitMQ connection using connection factory details as i mentioned above
                using IConnection connection = factory.CreateConnection();
                //Here we create channel with session and model
                using IModel channel = connection.CreateModel();
                channel.ConfirmSelect();
                var props = new HashMap<string, object>();
                //channel.QueueDeclare("images", exclusive: false);
                channel.QueueDeclare("images", false, false,true, props);
                
                //Serialize the message
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                //put the data on to the product queue
                channel.BasicPublish(exchange: "", routingKey: "images", body: body);
                channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(20));

            }
            catch (Exception e)
            {
                var a = e.Message;
                throw;
            }

            
            
            //declare the queue after mentioning name and a few property related to that

        }
    }
}
