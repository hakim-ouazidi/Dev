using GetShopWebAPI.Dtos;
using System;
using System.Collections.Generic;
namespace GetShopWebAPI.Repositories
{
   public interface IShopRepository
    {
        int DisLike(ShopDTO ShopDto);
        List<ShopDTO> GetLikedShops(string userId);
        List<ShopDTO> GetShops(string userId);
        int Like(ShopDTO ShopDto);
        int RemoveLike(ShopDTO ShopDto);
    }
}
