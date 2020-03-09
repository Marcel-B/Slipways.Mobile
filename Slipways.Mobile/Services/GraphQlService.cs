using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using Slipways.Mobile.Contracts;
using System.Threading.Tasks;

namespace Slipways.Mobile.Services
{
    public class GraphQLService : IGraphQLService
    {
        private IGraphQLClient _httpClient;

        public GraphQLService(
            IGraphQLClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> FetchValuesAsync<T>(
            string query)
        {
            var request = new GraphQLHttpRequest(query);
            var response = await _httpClient.SendQueryAsync<T>(request);
            return response.Data;
        }
    }
}
