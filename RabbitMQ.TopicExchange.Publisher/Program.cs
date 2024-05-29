
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://vwxwdwdh:DVMos4xd8TUPLFQK1N8E4SZ1EjCi9Jhc@chimpanzee.rmq.cloudamqp.com/vwxwdwdh");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Hello {i}");

    Console.Write("Specify the topic format in which the message will be sent : ");
    string topic = Console.ReadLine();

    channel.BasicPublish(exchange: "topic-exchange-example", routingKey: topic, body: message);
}

Console.Read();