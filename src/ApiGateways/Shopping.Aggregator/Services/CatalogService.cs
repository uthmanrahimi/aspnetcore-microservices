using Shopping.Aggregator.ExtensionMethods;
using Shopping.Aggregator.Models;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response = await _httpClient.GetAsync("/api/v1/Catalog");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Catalog/{id}");
            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Catalog/GetPRoductByCategory/{category}");
            return await response.ReadContentAs<List<CatalogModel>>();
        }
    }
}
