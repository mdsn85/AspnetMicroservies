using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configration)
        {
            _configuration = configration;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var connection = new NpgsqlConnection(
                _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("select * from Coupon where ProductName = @ProductName", new { ProductName = productName });

            if(coupon == null)
            {
                return new Coupon
                {
                    ProductName = productName,
                    Amount = 0,
                    Description = "No Discount Desc"
                };
            }

            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var connection = new NpgsqlConnection(
                              _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("insert into Coupon (ProductName,Amount,Description)  values (@ProductName,@Amount,@Description)",
                      new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount }
                );

            if (affected == 0)
                return false;

            return true; ;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var connection = new NpgsqlConnection(
                  _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("iDelete from Coupon  where ProductName = @ProductName)",
                      new { ProductName = productName }
                );

            if (affected == 0)
                return false;

            return true; ;
        }



        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
