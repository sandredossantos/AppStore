namespace AppStore.Domain.Interfaces
{
    public interface IRepositorySettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}