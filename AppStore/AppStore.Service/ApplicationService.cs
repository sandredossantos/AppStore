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

        public async Task<List<Application>> GetAll()
        {
            List<Application> applications = await _aplicationRepository.GetAll();

            return applications;
        }
        public async Task<Application> RegisterApplication(Application application)
        {
            await _aplicationRepository.Insert(application);

            return application;
        }
        public long BuyApp()
        {
            SendMessage();

            return 1;
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