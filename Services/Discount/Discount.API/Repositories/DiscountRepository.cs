using Dapper;

using Discount.API.Entities;

using Microsoft.Extensions.Configuration;

using Npgsql;

using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public async Task<bool> CreateAsync(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affectedRows = await connection.ExecuteAsync("INSERT INTO coupon(ProductName,Description,Amount) values (@productName,@description,@amount)",
                                        new { productName = coupon.ProductName, description = coupon.Description, amount = coupon.Amount }
                                        );
            return affectedRows > 0 ? true : false;
        }

        public async Task<bool> DeleteAsync(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affectedRows = await connection.ExecuteAsync("DELETE FROM coupon where ProductName=@ProductName", new { ProductName = productName });
            return affectedRows > 0 ? true : false;
        }

        public async Task<Coupon> GetDiscountAsync(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM coupon WHERE productName=@productName", new { productName = productName });
            if (coupon == null)
                return new Coupon { Description = "No Discount", Amount = 0 };
            return coupon;
        }

        public async Task<bool> UpdateAsync(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affectedRows = await connection.ExecuteAsync("UPDATE coupon set ProductName=@productName,Description=@description, Amount=@amount WHERE Id=@id ",
                new { productName = coupon.ProductName, description = coupon.Description, amount = coupon.Amount, id = coupon.Id });

            return affectedRows > 0 ? true : false;
        }
    }
}
