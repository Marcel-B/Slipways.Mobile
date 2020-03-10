using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ImTools;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Helpers;

namespace Slipways.Mobile.Data
{
    public class DataStore : IDataStore
    {
        private IGraphQLService _graphQLService;
        private IRepository _repository;

        public IEnumerable<Water> Waters { get; private set; }
        public IEnumerable<Slipway> Slipways { get; private set; }
        public IEnumerable<Manufacturer> Manufacturers { get; private set; }
        public IEnumerable<Extra> Extras { get; private set; }

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
            Waters = result.Waters;
            return Waters;
        }


        public async Task UpdateWatersAsync()
        {
            var watersResponse = await _graphQLService.FetchValuesAsync<WatersResponse>(Queries.Waters);
            var waters = watersResponse.Waters;

            foreach (var water in waters)
            {
                var tmp = _repository.GetByUuid<Water>(water.Pk);

                // Insert new Waters
                if (tmp == null)
                    _repository.Insert(water);

                // Insert Updated Waters
                else if (tmp.Updated != water.Updated)
                {
                    water.Id = tmp.Id;
                    _repository.Update(tmp.Id, water);
                }
            }
        }

        public async Task UpdateSlipwaysAsync()
        {
            var slipwaysResponse = await _graphQLService.FetchValuesAsync<SlipwaysResponse>(Queries.Slipways);
            var slipways = slipwaysResponse.Slipways;

            foreach (var slipway in slipways)
            {
                var tmp = _repository.GetByUuid<Slipway>(slipway.Pk);

                if (tmp == null)
                {
                    slipway.WaterPk = slipway.Water.Pk;
                    _repository.Insert(slipway);
                }
                else if (slipway.Updated != tmp.Updated)
                {
                    slipway.Id = tmp.Id;
                    slipway.WaterPk = slipway.Water.Pk;
                    _repository.Update(tmp.Id, slipway);
                }
            }
        }

        public async Task<IEnumerable<Manufacturer>> UpdateManufacturersAsync()
        {
            var manufacturersResponse = await _graphQLService.FetchValuesAsync<ManufacturersResponse>(Queries.Manufacturers);
            var manufacturers = manufacturersResponse.Manufacturers;

            foreach (var manufacturer in manufacturers)
            {
                var tmp = _repository.GetByUuid<Manufacturer>(manufacturer.Pk);
                if (tmp == null)
                    _repository.Insert(manufacturer);

                else if (manufacturer.Updated != tmp.Updated)
                {
                    manufacturer.Id = tmp.Id;
                    _repository.Update(tmp.Id, manufacturer);
                }
            }
            return _repository.GetAll<Manufacturer>();
        }


        public async Task<IEnumerable<Extra>> UpdateExtrasAsync()
        {
            var extraResponse = await _graphQLService.FetchValuesAsync<ExtrasResponse>(Queries.Extras);
            var extras = extraResponse.Extras;

            foreach (var extra in extras)
            {
                var tmp = _repository.GetByUuid<Extra>(extra.Pk);
                if (tmp == null)
                    _repository.Insert(extra);

                else if (extra.Updated != tmp.Updated)
                {
                    extra.Id = tmp.Id;
                    _repository.Update(tmp.Id, extra);
                }
            }
            return _repository.GetAll<Extra>();
        }

        public async Task UpdateAsync()
        {
            await UpdateExtrasAsync();
            await UpdateManufacturersAsync();
        }

        public void LoadData()
        {
            Manufacturers = _repository.GetAll<Manufacturer>();
            Extras = _repository.GetAll<Extra>();
            Waters = _repository.GetAll<Water>();
            var slipways = _repository.GetAll<Slipway>();

            foreach (var slipway in slipways)
                slipway.Water = Waters.First(_ => _.Pk == slipway.WaterPk);

            Slipways = slipways;
        }

        public async Task<IEnumerable<Slipway>> GetSlipwaysAsync()
        {
            if (Slipways != null)
                return Slipways;

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
                    waterId = _repository.Insert(slipway.Water);
                else
                    waterId = water.Id;

                slipway.Water.Id = waterId;
                slipway.WaterPk = slipway.Water.Pk;

                int slipwayId;

                var slipwayDb = _repository.GetByUuid<Slipway>(slipway.Pk);

                if (slipwayDb == null)
                {
                    slipwayId = _repository.Insert(slipway);
                }
                else
                {
                    slipwayId = slipwayDb.Id;
                }

                //Slipways.Add(slipway);
            }
            return Slipways;
        }
    }
}