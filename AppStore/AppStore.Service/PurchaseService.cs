using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace AppStore.Service
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task CreatePurchaseOrder(Purchase purchase)
        {
            purchase.Status = "Created";

            await _purchaseRepository.Insert(purchase);

            SendMessage(purchase.Id);
        }

        private void SendMessage(string message)
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

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "purchase",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}