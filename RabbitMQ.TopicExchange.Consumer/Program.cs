
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://vwxwdwdh:DVMos4xd8TUPLFQK1N8E4SZ1EjCi9Jhc@chimpanzee.rmq.cloudamqp.com/vwxwdwdh");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

Console.Write("Specify the topic format to be listened to: ");
string topic = Console.ReadLine();

string queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName, exchange: "topic-exchange-example", routingKey: topic);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();