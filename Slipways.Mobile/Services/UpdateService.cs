using Prism.Events;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data;
using Slipways.Mobile.Events;
using Slipways.Mobile.Helpers;
using System.Threading.Tasks;

namespace Slipways.Mobile.Services
{
    public class UpdateService : IUpdateService
    {
        private UpdateReadyEvent _updateReadyEvent;
        private IRepositoryWrapper _repositoryWrapper;
        private IGraphQLService _graphQLService;

        public UpdateService(
            IEventAggregator eventAggregator,
            IRepositoryWrapper repositoryWrapper,
            IGraphQLService graphQLService)
        {
            _updateReadyEvent = eventAggregator.GetEvent<UpdateReadyEvent>();

            _repositoryWrapper = repositoryWrapper;
            _graphQLService = graphQLService;
        }
        public async Task UpdateWater()
        {
            var response = await _graphQLService.FetchValuesAsync<WatersResponse>(Queries.Waters);
            foreach (var water in response.Waters)
                _repositoryWrapper.Waters.Insert(water);
            _updateReadyEvent.Publish("water");
        }
    }
}
