using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IWishlistBL
    {
        public WishListModel AddWishList(WishListModel wishlistModel, int userId);
        public bool DeleteWishList(int WishlistId, int userId);
        public List<ViewWishListModel> GetWishlistDetailsByUserid(int userId);
    }
}
