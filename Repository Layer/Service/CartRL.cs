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
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration Configuration { get; }
        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public CartModel AddCart(CartModel cartModel, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("AddCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderQuantity", cartModel.orderQuantity);
                    cmd.Parameters.AddWithValue("@BookId", cartModel.bookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return cartModel;
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
        public CartModel UpdateCart(int cartId, CartModel cartModel, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("UpdateCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderQuantity", cartModel.orderQuantity);
                    cmd.Parameters.AddWithValue("@BookId", cartModel.bookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@CartId", cartId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return cartModel;
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
        public bool DeleteCart(int cartId, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("DeleteCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", cartId);
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
        public List<ViewCartModel> GetCartDetailsByUserid(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("GetCartByUserId", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<ViewCartModel> cartmodels = new List<ViewCartModel>();
                        while (reader.Read())
                        {
                            BookModel bookModel = new BookModel();
                            ViewCartModel cartModel = new ViewCartModel();
                            bookModel.bookId = Convert.ToInt32(reader["BookId"]);
                            bookModel.bookName = reader["BookName"].ToString();
                            bookModel.authorName = reader["AuthorName"].ToString();
                            bookModel.actualPrice = Convert.ToInt32(reader["ActualPrice"]);
                            bookModel.discountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            bookModel.bookImage = reader["BookImage"].ToString();
                            cartModel.userId = Convert.ToInt32(reader["UserId"]);
                            cartModel.bookId = Convert.ToInt32(reader["BookId"]);
                            cartModel.cartId = Convert.ToInt32(reader["CartId"]);
                            cartModel.orderQuantity = Convert.ToInt32(reader["OrderQuantity"]);
                            cartModel.Bookmodel = bookModel;
                            cartmodels.Add(cartModel);
                        }

                        sqlConnection.Close();
                        return cartmodels;
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
