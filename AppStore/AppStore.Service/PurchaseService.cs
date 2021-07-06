using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using AppStore.Service.Language;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using static AppStore.Domain.Entities.Enums;

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
            if (CheckExistingPurchaseByCodeAndTaxNumber(purchase.Code, purchase.TaxNumber).Result == true)
                throw new Exception(string.Format(ServiceExceptionMsg.EXC0003, purchase.Code, purchase.TaxNumber));

            purchase.Status = Enum.GetName(typeof(PurchaseStatus), PurchaseStatus.Created);

            await _purchaseRepository.Insert(purchase);

            SendMessage(purchase.Id);
        }

        public void UpdateStatus(Purchase purchase, string status)
        {
            _purchaseRepository.UpdateStatus(purchase, status);
        }

        public Purchase GetById(string id)
        {
            Purchase purchase = _purchaseRepository.GetById(id);

            return purchase;
        }

        private async Task<bool> CheckExistingPurchaseByCodeAndTaxNumber(string code, string taxNumber)
        {
            Purchase purchase = await _purchaseRepository.GetByCodeAndTaxNumber(code, taxNumber);

            if (purchase != null) return true;

            return false;
        }

        private void SendMessage(string message)
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

                byte[] body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "purchase",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}