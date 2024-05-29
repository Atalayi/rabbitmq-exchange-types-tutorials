
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://vwxwdwdh:DVMos4xd8TUPLFQK1N8E4SZ1EjCi9Jhc@chimpanzee.rmq.cloudamqp.com/vwxwdwdh");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

while (true)
{
    Console.Write("Message : ");
    string? message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message ?? string.Empty);

    channel.BasicPublish(exchange: "direct-exchange-example", routingKey: "direct-queue-example", body: byteMessage);
}

Console.Read();