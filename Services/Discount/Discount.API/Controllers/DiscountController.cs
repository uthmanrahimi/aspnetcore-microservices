using Discount.API.Entities;
using Discount.API.Repositories;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("{productName}",Name ="GetDiscount")]
        public async Task<ActionResult<Coupon>> Get(string productName)
        {
            var coupon =await _discountRepository.GetDiscountAsync(productName);
            return Ok(coupon);
        }

        [HttpPost]
        public async Task<ActionResult<Coupon>> Create([FromBody] Coupon coupon)
        {
         await _discountRepository.CreateAsync(coupon);
            return CreatedAtAction("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        public async Task<ActionResult<Coupon>> Update([FromBody] Coupon coupon)
        {
           var result= await _discountRepository.CreateAsync(coupon);
            return Ok(result);
        }


        [HttpDelete("{productName}",Name ="DeleteDiscount")]
        public async Task<ActionResult<Coupon>> Delete(string productName)
        {
            var result = await _discountRepository.DeleteAsync(productName);
            return Ok(result);
        }

    }
}
