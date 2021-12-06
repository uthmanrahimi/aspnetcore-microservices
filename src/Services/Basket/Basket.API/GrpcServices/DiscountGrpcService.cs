using Discount.Grpc.Protos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _serviceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<CouponModel> GetCoupon(string productName)
        {
           return await _serviceClient.GetDiscountAsync(new GetDiscountRequest { ProductName=productName});
        }


    }
}
