using Shopping.Aggregator.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
