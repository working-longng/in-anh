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
                factory.UserName = "admin";
                factory.Password = "admin";
                factory.VirtualHost = "/";
                factory.HostName = "jinnie.shop";
                factory.Port = AmqpTcpEndpoint.UseDefaultPort;

                ConnectionFactory.DefaultAddressFamily = AddressFamily.InterNetwork;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //Create the RabbitMQ connection using connection factory details as i mentioned above
                using var connection = factory.CreateConnection();
                //Here we create channel with session and model
                using var channel = connection.CreateModel();

                var props = new HashMap<string, object>();
                //channel.QueueDeclare("images", exclusive: false);
                channel.QueueDeclareNoWait("images", false, false,true, props);
                //Serialize the message
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                //put the data on to the product queue
                channel.BasicPublish(exchange: "", routingKey: "images", body: body);

            }
            catch (Exception e)
            {

                throw;
            }

            
            
            //declare the queue after mentioning name and a few property related to that

        }
    }
}
