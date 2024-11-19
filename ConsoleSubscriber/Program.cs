// Console.WriteLine("Enter the Redis Channel you want to subscribe to:");
using MessageContract;
using StackExchange.Redis;
using System.Text.Json;

string topic = Console.ReadLine();

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
ISubscriber sub = redis.GetSubscriber();

await sub.SubscribeAsync(topic, (channel, message) => {
    TopicMessage receivedMessage = JsonSerializer.Deserialize<TopicMessage>(message);
    Console.WriteLine(receivedMessage.ToString());
});

Console.WriteLine($"Subscribed to {topic}, press any key to exit");
Console.ReadKey();