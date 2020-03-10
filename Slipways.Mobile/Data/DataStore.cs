using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;

namespace Slipways.Mobile.Data
{
    public class DataStore : IDataStore
    {
        private IRepositoryWrapper _repository;
        private IUpdateService _updateService;

        public IEnumerable<Water> Waters => _repository.Waters.GetAll();
        public IEnumerable<Slipway> Slipways => _repository.Slipways.GetAll();
        public IEnumerable<Manufacturer> Manufacturers => _repository.Manufacturers.GetAll();
        public IEnumerable<Extra> Extras => _repository.Extras.GetAll();
        public IEnumerable<Marina> Marinas => _repository.Marinas.GetAll();
        public IEnumerable<Service> Services => _repository.Services.GetAll();

        public DataStore(
            IRepositoryWrapper repository,
            IUpdateService updateService)
        {
            _repository = repository;
            _updateService = updateService;
        }

        //public async Task<IEnumerable<Water>> GetWatersAsync()
        //{
        //    if (Waters != null)
        //        return Waters;

        //    Waters = new List<Water>();

        //    var result = await _graphQLService.FetchValuesAsync<WatersResponse>(Queries.Waters);
        //    Waters = result.Waters;
        //    return Waters;
        //}

        //public async Task UpdateWatersAsync()
        //{
        //    var watersResponse = await _graphQLService.FetchValuesAsync<WatersResponse>(Queries.Waters);
        //    var waters = watersResponse.Waters;

        //    foreach (var water in waters)
        //    {
        //        var tmp = _repository.Waters.GetByUuid(water.Pk);

        //        // Insert new Waters
        //        if (tmp == null)
        //            _repository.Waters.Insert(water);

        //        // Insert Updated Waters
        //        else if (tmp.Updated != water.Updated)
        //        {
        //            water.Id = tmp.Id;
        //            _repository.Waters.Update(tmp.Id, water);
        //        }
        //    }
        //}

        //public async Task UpdateSlipwaysAsync()
        //{
        //    var slipwaysResponse = await _graphQLService.FetchValuesAsync<SlipwaysResponse>(Queries.Slipways);
        //    var slipways = slipwaysResponse.Slipways;

        //    foreach (var slipway in slipways)
        //    {
        //        var tmp = _repository.Slipways.GetByUuid(slipway.Pk);

        //        if (tmp == null)
        //        {
        //            slipway.WaterPk = slipway.Water.Pk;
        //            _repository.Slipways.Insert(slipway);
        //        }
        //        else if (slipway.Updated != tmp.Updated)
        //        {
        //            slipway.Id = tmp.Id;
        //            slipway.WaterPk = slipway.Water.Pk;
        //            _repository.Slipways.Update(tmp.Id, slipway);
        //        }
        //    }
        //}

        //public async Task<IEnumerable<Manufacturer>> UpdateManufacturersAsync()
        //{
        //    var manufacturersResponse = await _graphQLService.FetchValuesAsync<ManufacturersResponse>(Queries.Manufacturers);
        //    var manufacturers = manufacturersResponse.Manufacturers;

        //    foreach (var manufacturer in manufacturers)
        //    {
        //        var tmp = _repository.Manufacturers.GetByUuid(manufacturer.Pk);
        //        if (tmp == null)
        //            _repository.Manufacturers.Insert(manufacturer);

        //        else if (manufacturer.Updated != tmp.Updated)
        //        {
        //            manufacturer.Id = tmp.Id;
        //            _repository.Manufacturers.Update(tmp.Id, manufacturer);
        //        }
        //    }
        //    return _repository.Manufacturers.GetAll();
        //}

        //public async Task<IEnumerable<Extra>> UpdateExtrasAsync()
        //{
        //    var extraResponse = await _graphQLService.FetchValuesAsync<ExtrasResponse>(Queries.Extras);
        //    var extras = extraResponse.Extras;

        //    foreach (var extra in extras)
        //    {
        //        var tmp = _repository.Extras.GetByUuid(extra.Pk);
        //        if (tmp == null)
        //            _repository.Extras.Insert(extra);

        //        else if (extra.Updated != tmp.Updated)
        //        {
        //            extra.Id = tmp.Id;
        //            _repository.Extras.Update(tmp.Id, extra);
        //        }
        //    }
        //    return _repository.Extras.GetAll();
        //}

        //public async Task UpdateAsync()
        //{
        //    await UpdateExtrasAsync();
        //    await UpdateManufacturersAsync();
        //}

        /// <summary>
        /// Load Data from SQLite Database to Memory
        /// </summary>
        public async Task LoadData()
        {
            //_repository.Waters.GetAll();
            if (Waters == null || Waters.Count() == 0)
                await _updateService.UpdateWater();


            //Slipways = _repository.Slipways.GetAll();
            //Manufacturers = _repository.Manufacturers.GetAll();
            //Marinas = _repository.Marinas.GetAll();
            //Services = _repository.Services.GetAll();
            //Extras = _repository.Extras.GetAll();
        }

        //public async Task<IEnumerable<Slipway>> GetSlipwaysAsync()
        //{
        //    if (Slipways != null)
        //        return Slipways;

        //    await UpdateWatersAsync();
        //    await UpdateSlipwaysAsync();

        //    var slipways = _repository.Slipways.GetAll();
        //    if (slipways != null && slipways.Count > 0)
        //    {
        //        foreach (var slipway in slipways)
        //        {
        //            var water = _repository.Waters.GetByUuid(slipway.WaterPk);
        //            slipway.Water = water;
        //        }
        //        Slipways = slipways;
        //        return Slipways;
        //    }

        //    Slipways = new List<Slipway>();

        //    var response = await _graphQLService.FetchValuesAsync<SlipwaysResponse>(Queries.Slipways);
        //    foreach (var slipway in response.Slipways)
        //    {
        //        int waterId;
        //        var water = _repository.Waters.GetByUuid(slipway.Water.Pk);

        //        if (water == null)
        //            waterId = _repository.Waters.Insert(slipway.Water);
        //        else
        //            waterId = water.Id;

        //        slipway.Water.Id = waterId;
        //        slipway.WaterPk = slipway.Water.Pk;

        //        int slipwayId;

        //        var slipwayDb = _repository.Slipways.GetByUuid(slipway.Pk);

        //        if (slipwayDb == null)
        //        {
        //            slipwayId = _repository.Slipways.Insert(slipway);
        //        }
        //        else
        //        {
        //            slipwayId = slipwayDb.Id;
        //        }

        //        //Slipways.Add(slipway);
        //    }
        //    return Slipways;
        //}
    }
}