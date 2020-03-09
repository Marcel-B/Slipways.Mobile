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
        private IRepository _slipwaysDatabase;

        public IList<Slipway> Slipways { get; set; }

        public DataStore(
            IGraphQLService graphQLService,
            IRepository slipwaysDatabase)
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
                var water = _slipwaysDatabase.GetByUuid<Water>(slipway.Water.Pk);

                if (water == null)
                    waterId = _slipwaysDatabase.InsertOrUpdate(slipway.Water);
                else
                    waterId = water.Id;

                slipway.Water.Id = waterId;

                int slipwayId;

                var slipwayDb = _slipwaysDatabase.GetByUuid<Slipway>(slipway.Pk);

                if (slipwayDb == null)
                {
                    slipwayId = _slipwaysDatabase.InsertOrUpdate(slipway);
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