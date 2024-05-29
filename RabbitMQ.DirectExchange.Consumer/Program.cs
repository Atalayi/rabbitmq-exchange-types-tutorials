
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://vwxwdwdh:DVMos4xd8TUPLFQK1N8E4SZ1EjCi9Jhc@chimpanzee.rmq.cloudamqp.com/vwxwdwdh");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName, exchange: "direct-exchange-example", routingKey: "direct-queue-example");

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();


