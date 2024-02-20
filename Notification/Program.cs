using System.Text;
using Newtonsoft.Json;
using OrderApi.Repository.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new();
IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

channel.ExchangeDeclare("success_direct", type: ExchangeType.Direct);

var bindingKey = args[0];

var queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue:queueName,
    exchange: "success_direct",
    routingKey: bindingKey);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
    Console.WriteLine($"Order : {message} , RoutingKey : {ea.RoutingKey}");
};

channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);
if (bindingKey == "success")
{
    Console.WriteLine($"Program doğru olan siparişleri bekliyor");
}
else
{
    Console.WriteLine($"Program yanlış olan siparişleri bekliyor");
}



Console.ReadLine();
