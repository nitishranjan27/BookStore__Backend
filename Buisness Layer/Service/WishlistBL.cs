using Buisness_Layer.Interface;
using Common_Layer.Models;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Service
{
    public class WishlistBL : IWishlistBL
    {
        private readonly IWishlistRL wishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }

        public WishListModel AddWishList(WishListModel wishlistModel, int userId)
        {
            try
            {
                return this.wishlistRL.AddWishList(wishlistModel, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteWishList(int WishlistId, int userId)
        {
            try
            {
                return this.wishlistRL.DeleteWishList(WishlistId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ViewWishListModel> GetWishlistDetailsByUserid(int userId)
        {
            try
            {
                return this.wishlistRL.GetWishlistDetailsByUserid(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
