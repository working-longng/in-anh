using RabbitMQ.Client;
using System.Net.Sockets;
using System.Net;
using Lucene.Net.Support;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using In_Anh.Models.RabitMQModel;
using System.Text.RegularExpressions;
using ImageMagick;

namespace In_Anh.RabitMQ
{
    public class RabitMQConsumer : IHostedService
    {
       
            private IModel channel = null;
            private IConnection connection = null;

            // Initiate RabbitMQ and start listening to an input queue
            private void Run()
            {
                // ! Fill in your data here !
                var factory = new ConnectionFactory()
                {
                    HostName = "jinnie.shop",
                    // port = 5672, default value
                    VirtualHost = "/",
                    UserName = "guest",
                    Password = "guest"
                };

                this.connection = factory.CreateConnection();
                this.channel = this.connection.CreateModel();

                // ! Declare an exchange, need to be updated !
                //this.channel.ExchangeDeclare("exchange", "direct", true, false, null);

                // A queue to read messages
                this.channel.QueueDeclare(queue: "image",
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: true,
                                    arguments: null);
                //this.channel.QueueBind("queue.in", "exchange", "in");

                //// A queue to write messages
                //this.channel.QueueDeclare(queue: "queue.out",
                //                    durable: true,
                //                    exclusive: false,
                //                    autoDelete: false,
                //                    arguments: null);
                //this.channel.QueueBind("queue.out", "exchange", "out");

                this.channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(this.channel);
                consumer.Received += OnMessageRecieved;

                this.channel.BasicConsume(queue: "image",
                                    autoAck: false,
                                    consumer: consumer);
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                this.Run();
                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                this.channel.Dispose();
                this.connection.Dispose();
                return Task.CompletedTask;
            }

            // Publish a received  message with "reply:" prefix
            private void OnMessageRecieved(object model, BasicDeliverEventArgs args)
            {
                var body = args.Body.ToArray();
                //var message = Encoding.UTF8.GetString(body);
                //Console.WriteLine(" [x] Received {0}", message);

                //int dots = message.Split('.').Length - 1;

                //// Publish a response
                //string outMessage = "reply:" + message;
                //body = Encoding.UTF8.GetBytes(outMessage);
            var message = Encoding.UTF8.GetString(body);

            var data = JsonConvert.DeserializeObject<RabitMQSendData>(message);


            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = data.Path + fileName;

            using (var memoryStream = new MemoryStream(data.File))
            {


                string content = Encoding.UTF8.GetString(data.File);
                if (Regex.IsMatch(content, @"<script|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {

                }

                if (!Directory.Exists(data.Path))
                {
                    Directory.CreateDirectory(data.Path);
                }


                using (FileStream f = File.Create(filePath))
                {

                    f.Dispose();
                }
                memoryStream.Position = 0;
                using (MagickImage image = new MagickImage(memoryStream))
                {
                    image.Format = MagickFormat.Jpeg;
                    image.Quality = 100;
                    image.Write(filePath);
                    image.Dispose();
                }
            }
            //this.channel.BasicPublish(exchange: "exchange",
            //                     routingKey: "out",
            //                     basicProperties: this.channel.CreateBasicProperties(),
            //                     body: body);
            //Console.WriteLine(" [x] Sent {0}", outMessage);

            Console.WriteLine(" [x] Done");
                this.channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
            }

        }
}
