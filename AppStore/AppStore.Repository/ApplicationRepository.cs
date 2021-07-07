using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IMongoCollection<Application> _applicationCollection;

        public ApplicationRepository(IRepositorySettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _applicationCollection = database.GetCollection<Application>(nameof(Application));
        }

        public async Task<List<Application>> GetAll()
        {
            return await _applicationCollection.Find(a => true).ToListAsync();
        }

        public async Task<Application> GetByCode(string code)
        {
            Application application = await _applicationCollection.Find(
                a => a.Code == code
                ).FirstOrDefaultAsync();

            return application;
        }

        public async Task<Application> Insert(Application application)
        {
            await _applicationCollection.InsertOneAsync(application);

            return application;
        }
    }
}