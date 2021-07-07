using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using AppStore.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using static AppStore.Domain.Entities.Enums;

namespace AppStore.Consumer
{
    class Worker
    {
        private readonly IPurchaseService _purchaseService;

        private static IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddEnvironmentVariables()
               .Build();

        public Worker(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<Worker>().Run();
        }

        public void Run()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "purchase",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);

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

        private void Process(string message)
        {
            Purchase purchase = _purchaseService.GetById(message);

            string status = Enum.GetName(typeof(PurchaseStatus), PurchaseStatus.Processed);

            _purchaseService.UpdateStatus(purchase, status);
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<Worker>();
                    services.AddTransient<IPurchaseService, PurchaseService>();
                    services.ConfigureRepositoryServices(configuration);
                });
        }
    }
}