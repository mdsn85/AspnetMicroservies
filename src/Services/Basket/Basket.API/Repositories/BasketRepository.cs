using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository:IBasketRepository
    {
        private readonly IDistributedCache _redisCash;

        public BasketRepository(IDistributedCache redisCash)
        {
            _redisCash = redisCash;
        }



        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCash.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject <ShoppingCart> (basket);

        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCash.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string UserName)
        {
            await _redisCash.RemoveAsync(UserName);
            
        }
    }
}
