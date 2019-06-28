using GetShopWebAPI.Dtos;
using GetShopWebAPI.Models;
using GetShopWebAPI.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http;

namespace GetShopWebAPI.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly ApplicationDbContext _context;
        public ShopRepository (ApplicationDbContext context)
	    {
            _context=context;
	    }

        public List<ShopDTO> GetShops(string userId)
        {
         DateTime hoursCondition = DateTime.Now.AddHours(-2);
         var shops = from s in _context.Shops
                        where !(_context.UserShops
                            .Where(o => o.UserId == userId && o.Like == true || 
                                    (o.Dislike == true && o.ActionDate > hoursCondition && o.UserId == userId))
                            .Select(b => b.ShopId).ToList()).Contains(s.Id)
                        select s; 
         return  shops.Select(q=>new ShopDTO()
                {Id=q.Id,
                    Name=q.Name,
                    UserId=userId,
                    Distance=q.Distance,
                    Adresse=q.Adresse
                }).OrderBy(_=>_.Distance).ToList();
        }

        public List<ShopDTO> GetLikedShops(string userId)
        {
          var shops = _context.UserShops
                       .Include(_=>_.Shop)
                       .Where(o => o.UserId == userId && o.Like == true)
                       .Select(b => b.Shop)
                       .ToList();
          return  shops.Select(q=>new ShopDTO()
                {Id=q.Id,
                    Name=q.Name,
                    UserId=userId,
                    Distance=q.Distance,
                    Adresse=q.Adresse
                }).ToList();
        }

        public int RemoveLike(ShopDTO ShopDto)
        {
           var userShop = GetShop(ShopDto);
            if (userShop != null)
            {
                userShop.Like = false;
                userShop.ActionDate = DateTime.Now;
                _context.SaveChanges();
            }
             return userShop.ShopId;
        }

        public int DisLike(ShopDTO ShopDto)
        {
            var userShop = GetShop(ShopDto);
            if (userShop != null)
            {
                userShop.Dislike = true;
                userShop.Like = false;
                userShop.ActionDate = DateTime.Now;
            }
            else
            {
               CreateShop(ShopDto);
            }
          _context.SaveChanges();
          return ShopDto.Id;
        }

        public int Like(ShopDTO ShopDto)
        {
            var userShop = GetShop(ShopDto);
            if (userShop != null)
            {
                userShop.Dislike = false;
                userShop.Like = true;
                userShop.ActionDate = DateTime.Now;
            }
            else
            {
                CreateShop(ShopDto);                
            }
            _context.SaveChanges();
            return ShopDto.Id;
        }

        private UserShop GetShop(ShopDTO ShopDto)
        {
            var userShop = _context.UserShops
                       .SingleOrDefault(o => o.UserId == ShopDto.UserId
                                         && o.ShopId == ShopDto.Id);
            return userShop;
        }

        private void CreateShop(ShopDTO ShopDto)
        {
            var newUserShop = new UserShop
            {
                ActionDate = DateTime.Now,
                Dislike = false,
                Like = true,
                ShopId = ShopDto.Id,
                UserId = ShopDto.UserId
            };
          _context.UserShops.Add(newUserShop);
        }
    }
}