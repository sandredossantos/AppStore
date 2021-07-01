using AppStore.Domain.Interfaces;

namespace AppStore.Repository
{
    public class RepositorySettings : IRepositorySettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}