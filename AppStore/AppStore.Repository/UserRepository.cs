using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace AppStore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(IRepositorySettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userCollection = database.GetCollection<User>(nameof(User));
        }    

        public async Task<User> Insert(User user)
        {
            await _userCollection.InsertOneAsync(user);

            return user;
        }

        public async Task<User> GetByTaxNumber(string taxNumber)
        {
            User user = await _userCollection.Find(
                user => user.TaxNumber == taxNumber
                ).FirstOrDefaultAsync();

            return user;
        }
    }
}