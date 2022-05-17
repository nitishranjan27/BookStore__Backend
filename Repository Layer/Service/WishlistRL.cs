using Common_Layer.Models;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository_Layer.Service
{
    public class WishlistRL : IWishlistRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration Configuration { get; }
        public WishlistRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public WishListModel AddWishList(WishListModel wishlistModel, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("AddWishList", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", wishlistModel.bookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return wishlistModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteWishList(int WishlistId, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("DeleteWishList", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@WishListId", WishlistId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ViewWishListModel> GetWishlistDetailsByUserid(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetWishListByUserId", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<ViewWishListModel> Wishlistmodels = new List<ViewWishListModel>();
                        while (reader.Read())
                        {
                            BookModel bookModel = new BookModel();
                            ViewWishListModel WishlistModel = new ViewWishListModel();
                            bookModel.bookId = Convert.ToInt32(reader["BookId"]);
                            bookModel.bookName = reader["BookName"].ToString();
                            bookModel.authorName = reader["AuthorName"].ToString();
                            bookModel.actualPrice = Convert.ToInt32(reader["ActualPrice"]);
                            bookModel.discountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            bookModel.bookImage = reader["BookImage"].ToString();
                            WishlistModel.userId = Convert.ToInt32(reader["UserId"]);
                            WishlistModel.bookId = Convert.ToInt32(reader["BookId"]);
                            WishlistModel.WishlistId = Convert.ToInt32(reader["WishListId"]);
                            WishlistModel.Bookmodel = bookModel;
                            Wishlistmodels.Add(WishlistModel);
                        }

                        sqlConnection.Close();
                        return Wishlistmodels;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
