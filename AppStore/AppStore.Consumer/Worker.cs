using AppStore.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;

namespace AppStore.Consumer
{
    class Worker
    {
        private static IMongoCollection<Purchase> _purchaseCollection;
        static void Main(string[] args)
        {
            ConsumeQueue();
        }
        private static void ConsumeQueue()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "purchase",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Process(message);

                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "purchase",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
        private static void Process(string message)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile($"appsettings.json");
            var configuration = builder.Build();

            var client = new MongoClient(configuration.GetConnectionString("ConnectionString"));
            var dataBase = client.GetDatabase("AppStore");

            _purchaseCollection = dataBase.GetCollection<Purchase>(nameof(Purchase));

            Purchase purchase = _purchaseCollection.Find(
                p => p.Id == message
                ).FirstOrDefault();

            var filter = Builders<Purchase>.Filter.Eq("Id", message);
            var update = Builders<Purchase>.Update.Set("Status", "Processed");

            _purchaseCollection.UpdateOne(filter, update);
        }
    }
}