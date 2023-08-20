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
factory.Uri = new Uri("amqp://guest:guest@jinnie.shop/%2f");

//Create the RabbitMQ connection using connection factory details as i mentioned above


//channel.QueueDeclareNoWait("images", false, false, true, props);

//Set Event object which listen message from chanel which is sent by producer



