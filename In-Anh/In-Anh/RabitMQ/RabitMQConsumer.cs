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
		private void Run()
		{
			var factory = new ConnectionFactory()
			{
				HostName = "jinnie.shop",
				VirtualHost = "/",
				UserName = "admin",
				Password = "admin"
			};
			connection = factory.CreateConnection();
			channel = connection.CreateModel();
			channel.QueueDeclare(queue: "image",
								durable: true,
								exclusive: false,
								autoDelete: false,
								arguments: null);
			channel.BasicQos(prefetchSize: 0, prefetchCount: 5, global: false);
			Console.WriteLine(" [*] Waiting for messages.");
			var consumer = new EventingBasicConsumer(this.channel);
			consumer.Received += OnMessageRecieved;
			channel.BasicConsume(queue: "image",
								autoAck: false,
								consumer: consumer);
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			Run();
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			channel.Dispose();
			connection.Dispose();
			return Task.CompletedTask;
		}

		private void OnMessageRecieved(object model, BasicDeliverEventArgs args)
		{
			var body = args.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);
			var data = JsonConvert.DeserializeObject<RabitMQSendData>(message);
			var fileName = data.FileName;
			var filePath = data.Path + "\\" + fileName;
			using (var memoryStream = new MemoryStream(data.File))
			{
				string content = Encoding.UTF8.GetString(data.File);
				if (Regex.IsMatch(content, @"<script|<cross\-domain\-policy",
					RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
				{
					return;
				}
				if (!Directory.Exists(data.Path))
				{
					Directory.CreateDirectory(data.Path);
				}
				if (!File.Exists(filePath))
				{
					using (FileStream f = File.Create(filePath))
					{
						f.Dispose();
					}
				}
				memoryStream.Position = 0;
				using (MagickImage image = new MagickImage(memoryStream))
				{
					image.Format = MagickFormat.Jpeg;
					image.Quality = 100;
					image.Write(filePath);
					image.Dispose();
				}
				memoryStream.Close();
			}
			Console.WriteLine(" [x] Done");
			channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
		}

	}
}
