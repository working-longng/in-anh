using ImageMagick;
using In_Anh.Models.RabitMQModel;
using Microsoft.IO;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RabitMQConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "jinnie.shop"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            using (var connection = factory.CreateConnection())
            //Here we create channel with session and model
            using (var channel = connection.CreateModel())
            {
                //declare the queue after mentioning name and a few property related to that
                channel.QueueDeclare("images", exclusive: false);
                //Set Event object which listen message from chanel which is sent by producer
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, eventArgs) => {
                    var body = eventArgs.Body.ToArray();
                    
                    var message = Encoding.UTF8.GetString(body);

                    var data = JsonConvert.DeserializeObject<RabitMQSendData>(message);
                    
                    try
                    {
                                var fileName = Guid.NewGuid().ToString() + ".jpg";
                                var filePath = data.path + fileName;

                                using (var memoryStream = new MemoryStream(data.File))
                                {
                                  
                                    
                                    string content = Encoding.UTF8.GetString(data.File);
                                    if (Regex.IsMatch(content, @"<script|<cross\-domain\-policy",
                                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                                    {
                                       
                                    }

                                    if (!Directory.Exists(data.path))
                                    {
                                        Directory.CreateDirectory(data.path);
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
                    }
                    catch (Exception e)
                    {

                       

                    }

                    Console.WriteLine("Success");

                };
                //read the message

                channel.BasicConsume(queue: "images", autoAck: false, consumer: consumer);
                Console.ReadKey();
                
                

            }


        }
    }
}
