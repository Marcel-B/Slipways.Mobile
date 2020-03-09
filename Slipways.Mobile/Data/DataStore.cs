using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Helpers;

namespace Slipways.Mobile.Data
{
    public class DataStore : IDataStore
    {
        private IGraphQLService _graphQLService;
        private IRepository _repository;

        public IList<Slipway> Slipways { get; set; }
        public IList<Water> Waters { get; set; }

        public DataStore(
            IGraphQLService graphQLService,
            IRepository slipwaysDatabase)
        {
            _graphQLService = graphQLService;
            _repository = slipwaysDatabase;
        }

        public async Task<IEnumerable<Water>> GetWatersAsync()
        {
            if (Waters != null)
                return Waters;

            Waters = new List<Water>();

            var result = await _graphQLService.FetchValuesAsync<WatersResponse>(Queries.Waters);
            foreach (var water in result.Waters)
            {
                Waters.Add(water);
            }
            return Waters;
        }


        public async Task UpdateWatersAsync()
        {
            var watersResponse = await _graphQLService.FetchValuesAsync<WatersResponse>(Queries.Waters);
            var waters = watersResponse.Waters;

            foreach (var water in waters)
            {
                _repository.InsertOrUpdate(water);
            }
        }

        public async Task UpdateSlipwaysAsync()
        {
            var slipwaysResponse = await _graphQLService.FetchValuesAsync<SlipwaysResponse>(Queries.Slipways);
            var slipways = slipwaysResponse.Slipways;

            foreach (var slipway in slipways)
            {
                var slip = _repository.GetByUuid<Slipway>(slipway.Pk);

                if (slip != null)
                    slipway.Id = slip.Id;

                slipway.WaterPk = slipway.Water.Pk;
                _repository.InsertOrUpdate(slipway);
            }
        }

        public async Task UpdateManufacturersAsync()
        {
            var manufacturersResponse = await _graphQLService.FetchValuesAsync<ManufacturersResponse>(Queries.Manufacturers);
            var manufacturers = manufacturersResponse.Manufacturers;

            foreach (var manufacturer in manufacturers)
                _repository.InsertOrUpdate(manufacturer);
        }

        public async Task<IEnumerable<Slipway>> GetSlipwaysAsync()
        {
            if (Slipways != null)
                return Slipways;

            await UpdateManufacturersAsync();
            await UpdateWatersAsync();
            await UpdateSlipwaysAsync();

            var slipways = _repository.GetAll<Slipway>();
            if (slipways != null && slipways.Count > 0)
            {
                foreach (var slipway in slipways)
                {
                    var water = _repository.GetByUuid<Water>(slipway.WaterPk);
                    slipway.Water = water;
                }
                Slipways = slipways;
                return Slipways;
            }

            Slipways = new List<Slipway>();

            var response = await _graphQLService.FetchValuesAsync<SlipwaysResponse>(Queries.Slipways);
            foreach (var slipway in response.Slipways)
            {
                int waterId;
                var water = _repository.GetByUuid<Water>(slipway.Water.Pk);

                if (water == null)
                    waterId = _repository.InsertOrUpdate(slipway.Water);
                else
                    waterId = water.Id;

                slipway.Water.Id = waterId;
                slipway.WaterPk = slipway.Water.Pk;

                int slipwayId;

                var slipwayDb = _repository.GetByUuid<Slipway>(slipway.Pk);

                if (slipwayDb == null)
                {
                    slipwayId = _repository.InsertOrUpdate(slipway);
                }
                else
                {
                    slipwayId = slipwayDb.Id;
                }

                Slipways.Add(slipway);
            }
            return Slipways;
        }
    }
}