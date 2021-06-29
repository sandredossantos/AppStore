using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppStore.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IAplicationRepository _aplicationRepository;

        public ApplicationService(IAplicationRepository aplicationRepository)
        {
            _aplicationRepository = aplicationRepository;
        }

        public async Task<List<Application>> GetAllApps()
        {
            List<Application> applications = await _aplicationRepository.GetAllApps();

            return applications;
        }
        public long BuyApp()
        {
            SendMessage();

            return 1;
        }

        public async Task<Application> RegisterApplication(Application application)
        {
            await _aplicationRepository.RegisterApplication(application);

            return application;
        }

        private void SendMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}