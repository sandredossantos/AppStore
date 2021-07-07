using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace AppStore.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly IMongoCollection<Purchase> _purchaseCollection;
        public PurchaseRepository(IRepositorySettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _purchaseCollection = database.GetCollection<Purchase>(nameof(Purchase));
        }

        public async Task<Purchase> GetByCodeAndTaxNumber(string code, string taxNumber)
        {
            Purchase purchase = await _purchaseCollection.Find(
               p => (p.Code == code) && (p.TaxNumber == taxNumber)
               ).FirstOrDefaultAsync();

            return purchase;
        }

        public async Task<Purchase> Insert(Purchase purchase)
        {
            await _purchaseCollection.InsertOneAsync(purchase);

            return purchase;
        }

        public void UpdateStatus(Purchase purchase, string status)
        {
            var filter = Builders<Purchase>.Filter.Eq("Id", purchase.Id);
            var update = Builders<Purchase>.Update.Set("Status", status);

            _purchaseCollection.UpdateOne(filter, update);
        }
        public Purchase GetById(string id)
        {
            Purchase purchase = _purchaseCollection.Find(
               p => p.Id == id
               ).FirstOrDefault();

            return purchase;
        }
    }
}