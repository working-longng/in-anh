// See https://aka.ms/new-console-template for more information
using ImageMagick;
using In_Anh.Models.RabitMQModel;
using Lucene.Net.Support;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://admin:admin@jinnie.shop/%2f");

ConnectionFactory.DefaultAddressFamily = AddressFamily.InterNetwork;
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
//Create the RabbitMQ connection using connection factory details as i mentioned above
using var connection = factory.CreateConnection();
//Here we create channel with session and model
using var channel = connection.CreateModel();
channel.BasicQos(0, 1, false);
var props = new HashMap<string, object>();
//channel.QueueDeclare("images", exclusive: false);
channel.QueueDeclareNoWait("images", false, false, true, props);

//Set Event object which listen message from chanel which is sent by producer
var consumer = new EventingBasicConsumer(channel);

try
{
    consumer.Received += (model, eventArgs) => {
        var body = eventArgs.Body.ToArray();

        var message = Encoding.UTF8.GetString(body);

        var data = JsonConvert.DeserializeObject<RabitMQSendData>(message);


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



        Console.WriteLine("Success");

    };
    //read the message

    channel.BasicConsume(queue: "images", autoAck: true, consumer: consumer);
    Console.ReadKey();
}
catch (Exception)
{

    throw;
}



