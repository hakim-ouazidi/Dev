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

namespace GetShopWebAPI.Controllers
{
    [RoutePrefix("api/Shop")]
    public class ShopController : ApiController
    {
       
        private readonly IShopRepository _shopRepository;

        public ShopController(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        /// <summary>
        /// Returns the list of shops sorted by distance
        /// </summary>
        /// <returns>List of ShopDTO</returns>
        public IHttpActionResult Get()
        {   var userId = User.Identity.GetUserId();
            var shops=new List<ShopDTO>();
            try
            {
                shops = _shopRepository.GetShops(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(shops);
        }
       
        /// <summary>
        /// Returns the list of preferred shops for a user
        /// </summary>
        /// <returns>List of ShopDTO</returns>

        [Route("GetLikedShop")]
        public IHttpActionResult GetLikedShop()
        {
            var userId = User.Identity.GetUserId();
            var shops=new List<ShopDTO>();
            try
            {
                shops = _shopRepository.GetLikedShops(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(shops);
        }

        /// <summary>
        ///  Remove a shop form preferred  shops for a user
        /// </summary>
        /// <param name="ShopDto">ShopDto</param>
        /// <returns></returns>
        public IHttpActionResult RemoveLike([FromBody]ShopDTO ShopDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _shopRepository.RemoveLike(ShopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Dislike a shop
        /// </summary>
        /// <param name="ShopDto">ShopDto</param>
        /// <returns></returns>
        [Route("DisLike")]
        public IHttpActionResult DisLike([FromBody]ShopDTO ShopDto)
        {   
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _shopRepository.DisLike(ShopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Like a shop
        /// </summary>
        /// <param name="ShopDto">ShopDto</param>
        /// <returns></returns>
        [Route("Like")]
        public IHttpActionResult Like([FromBody]ShopDTO ShopDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _shopRepository.Like(ShopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }

}