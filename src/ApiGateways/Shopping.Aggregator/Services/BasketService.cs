using Shopping.Aggregator.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<BasketModel> GetBasket(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
