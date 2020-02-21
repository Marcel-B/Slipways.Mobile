using GraphQL.Client.Http;
using Slipways.Mobile.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slipways.Mobile.Services
{
    public class GraphQlService
    {
        private GraphQLHttpClient _httpClient;

        public GraphQlService(
            GraphQLHttpClient httpClient = null)
        {
            _httpClient = httpClient ?? new GraphQLHttpClient("https://data.slipways.de/graphql");
        }

        public async Task<IEnumerable<Slipway>> GetSlipwaysASync()
        {
            var query = "{ slipways { name id city water { id longname } } }";
            var request = new GraphQLHttpRequest
            {
                Query = query
            };

            var response = await _httpClient.SendQueryAsync<SlipwaysResponse>(request);
            return response.Data.Slipways;
        }
    }
}
