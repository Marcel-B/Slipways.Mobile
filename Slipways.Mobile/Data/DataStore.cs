using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Helpers;

namespace Slipways.Mobile.Data
{
    public class DataStore : IDataStore
    {
        private IGraphQLService _graphQLService;
        private ISlipwaysDatabase _slipwaysDatabase;

        public IList<Slipway> Slipways { get; set; }

        public DataStore(
            IGraphQLService graphQLService,
            ISlipwaysDatabase slipwaysDatabase)
        {
            _graphQLService = graphQLService;
            _slipwaysDatabase = slipwaysDatabase;
        }

        public async Task<IEnumerable<Slipway>> GetSlipwaysAsync()
        {
            if (Slipways != null)
                return Slipways;

            Slipways = new List<Slipway>();

            var response = await _graphQLService.FetchValuesAsync<SlipwaysResponse>(Queries.Slipways);
            foreach (var slipway in response.Slipways)
            {
                int waterId;
                var water = await _slipwaysDatabase.GetByUuidAsync<Water>(slipway.Water.Pk);

                if (water == null)
                    waterId = await _slipwaysDatabase.SaveRecordAsync(slipway.Water);
                else
                    waterId = water.Id;

                slipway.Water.Id = waterId;

                int slipwayId;

                var slipwayDb = await _slipwaysDatabase.GetByUuidAsync<Slipway>(slipway.Pk);

                if (slipwayDb == null)
                {
                    slipwayId = await _slipwaysDatabase.SaveRecordAsync(slipway);
                }
                else
                {
                    slipwayId = slipwayDb.Id;
                }

                Slipways.Add(slipway);
                System.Console.WriteLine($"SlipwayId is {slipwayId}");
            }
            return Slipways;
        }
    }
}