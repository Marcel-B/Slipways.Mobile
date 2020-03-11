using Prism.Events;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Events;
using Slipways.Mobile.Helpers;
using System;
using System.Linq;
using System.Runtime.InteropServices;
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

        public Task UpdateExtra()
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateMarina()
        {
            var response = await _graphQLService
                .FetchValuesAsync<MarinasResponse>(Queries.Marinas)
                .ConfigureAwait(false);

            foreach (var marina in response.Ports.OrderBy(_ => _.Name))
            {
                marina.WaterPk = marina.Water.Pk;
                var tmp = _repositoryWrapper.Marinas.GetByUuid(marina.Pk);
                if (tmp == null)
                {
                    _repositoryWrapper.Marinas.Insert(marina);
                }
                else if (tmp.Updated != marina.Updated)
                {
                    marina.Id = tmp.Id;
                    _repositoryWrapper.Marinas.Update(tmp.Id, marina);
                }
            }
            _updateReadyEvent.Publish("marina");
        }

        public async Task UpdateManufacturer()
        {
            var response = await _graphQLService
                .FetchValuesAsync<ManufacturersResponse>(Queries.Manufacturers)
                .ConfigureAwait(false);

            foreach (var manufacturer in response.Manufacturers)
                _repositoryWrapper.Manufacturers.Insert(manufacturer);
            _updateReadyEvent.Publish("manufacturer");
        }

        public async Task UpdateSlipway()
        {
            var response = await _graphQLService
                .FetchValuesAsync<SlipwaysResponse>(Queries.Slipways)
                .ConfigureAwait(false);

            foreach (var slipway in response.Slipways)
            {
                slipway.WaterPk = slipway.Water.Pk;
                slipway.MarinaPk = slipway.Marina == null ? Guid.Empty : slipway.Marina.Pk;
                var tmp = _repositoryWrapper.Slipways.GetByUuid(slipway.Pk);

                if (tmp == null)
                {
                    _repositoryWrapper.Slipways.Insert(slipway);
                }
                else if (tmp.Updated != slipway.Updated)
                {
                    slipway.Id = tmp.Id;
                    _repositoryWrapper.Slipways.Update(slipway.Id, slipway);
                }
            }
            _updateReadyEvent.Publish("slipway");
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
