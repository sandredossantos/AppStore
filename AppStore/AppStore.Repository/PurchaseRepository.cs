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
        public async Task<Purchase> Insert(Purchase purchase)
        {
            await _purchaseCollection.InsertOneAsync(purchase);

            return purchase;
        }
    }
}