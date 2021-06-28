using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Repository
{
    public class ApplicationRepository : IAplicationRepository
    {
        private readonly IMongoCollection<Application> _applicationCollection;

        public ApplicationRepository(IRepositorySettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _applicationCollection = database.GetCollection<Application>(settings.CollectionName);
        }

        public async Task<List<Application>> GetAllApps()
        {
            return await _applicationCollection.Find(a => true).ToListAsync();
        }

        public async Task<Application> RegisterApplication(Application application)
        {
            await _applicationCollection.InsertOneAsync(application);

            return application;
        }
    }
}