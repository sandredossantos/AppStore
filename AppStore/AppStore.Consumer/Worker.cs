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

namespace AppStore.Consumer
{
    class Worker
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IUserService _userService;

        private static IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddEnvironmentVariables()
               .Build();

        public Worker(IPurchaseService purchaseService, IUserService userService)
        {
            _purchaseService = purchaseService;
            _userService = userService;
        }
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<Worker>().Run();
        }

        public void Run()
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

        private void Process(string message)
        {
            Purchase purchase = _purchaseService.GetById(message);

            if (purchase != null)
                _purchaseService.UpdateStatus(purchase, "Processed");
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<Worker>();

                    services.AddTransient<IPurchaseService, PurchaseService>();
                    services.AddTransient<IUserService, UserService>();
                    services.ConfigureRepositoryServices(configuration);                    
                });
        }
    }
}