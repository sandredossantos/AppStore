using AppStore.Domain.Entities;
using AppStore.Domain.Interfaces;
using System.Collections.Generic;
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

        public async Task<List<Application>> GetAllApps()
        {
            List<Application> applications = await _aplicationRepository.GetAll();

            return applications;
        }
        public async Task<Application> RegisterApplication(Application application)
        {
            await _aplicationRepository.Insert(application);

            return application;
        }
    }
}