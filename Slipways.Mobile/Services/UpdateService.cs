using Prism.Events;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Events;
using Slipways.Mobile.Helpers;
using Slipways.Mobile.ViewModels;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Slipways.Mobile.Services
{
    public class UpdateService : IUpdateService
    {
        private IEventAggregator _eventAggregator;
        private IRepositoryWrapper _repositoryWrapper;
        private IGraphQLService _graphQLService;

        public UpdateService(
            IEventAggregator eventAggregator,
            IRepositoryWrapper repositoryWrapper,
            IGraphQLService graphQLService)
        {
            _eventAggregator = eventAggregator;
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
                var tmp = await _repositoryWrapper.Marinas.GetByUuidAsync(marina.Pk);
                if (tmp == null)
                {
                    await _repositoryWrapper.Marinas.InsertAsync(marina);
                }
                else if (tmp.Updated != marina.Updated)
                {
                    marina.Id = tmp.Id;
                    await _repositoryWrapper.Marinas.UpdateAsync(tmp.Id, marina);
                }
            }

            var eventArgs = new DataUpdateEventArgs<Marina>
            {
                Type = "marina",
                Data = await _repositoryWrapper.Marinas.GetAllAsync()
            };

            _eventAggregator
                .GetEvent<UpdateReadyEvent<Marina>>()
                .Publish(eventArgs);
        }

        public async Task UpdateManufacturer()
        {
            //var response = await _graphQLService
            //    .FetchValuesAsync<ManufacturersResponse>(Queries.Manufacturers)
            //    .ConfigureAwait(false);

            //foreach (var manufacturer in response.Manufacturers)
            //    await _repositoryWrapper.Manufacturers.InsertAsync(manufacturer);
            //_updateReadyEvent.Publish("manufacturer");
        }

        public async Task UpdateSlipway()
        {
            //var response = await _graphQLService
            //    .FetchValuesAsync<SlipwaysResponse>(Queries.Slipways)
            //    .ConfigureAwait(false);

            //foreach (var slipway in response.Slipways)
            //{
            //    slipway.WaterPk = slipway.Water.Pk;
            //    slipway.MarinaPk = slipway.Marina == null ? Guid.Empty : slipway.Marina.Pk;
            //    var tmp = await _repositoryWrapper.Slipways.GetByUuidAsync(slipway.Pk);

            //    if (tmp == null)
            //    {
            //        await _repositoryWrapper.Slipways.InsertAsync(slipway);
            //    }
            //    else if (tmp.Updated != slipway.Updated)
            //    {
            //        slipway.Id = tmp.Id;
            //        await _repositoryWrapper.Slipways.UpdateAsync(slipway.Id, slipway);
            //    }
            //}
            //_updateReadyEvent.Publish("slipway");
        }

        public async Task UpdateStation()
        {
            //var response = await _graphQLService.FetchValuesAsync<StationResponse>(Queries.Stations);
            //foreach (var station in response.Stations)
            //{
            //    station.WaterPk = station?.Water.Pk ?? Guid.Empty;
            //    await _repositoryWrapper.Stations.InsertAsync(station);
            //}
            //_updateReadyEvent.Publish("station");
        }

        public async Task UpdateWater()
        {
            var response = await _graphQLService.FetchValuesAsync<WatersResponse>(Queries.Waters);
            foreach (var water in response.Waters)
                await _repositoryWrapper.Waters.InsertAsync(water);

            var eventArgs = new DataUpdateEventArgs<Water>
            {
                Type = "water",
                Data = await _repositoryWrapper.Waters.GetAllAsync()
            };
            _eventAggregator
                .GetEvent<UpdateReadyEvent<Water>>()
                .Publish(eventArgs);
        }
    }
}
