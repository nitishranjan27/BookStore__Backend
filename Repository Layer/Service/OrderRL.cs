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
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public OrderModel AddOrder(OrderModel orderModel, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);

            try
            {
                SqlCommand cmd = new SqlCommand("AddOrders", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookQuantity", orderModel.BookQuantity);
                cmd.Parameters.AddWithValue("@BookId", orderModel.BookId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@AddressId", orderModel.AddressId);

                sqlConnection.Open();
                var result = Convert.ToInt32(cmd.ExecuteScalar());
                sqlConnection.Close();

                if (result != 2 && result != 3)
                {
                    return orderModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OrderResponse> GetAllOrder(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                List<OrderResponse> ordersResponse = new List<OrderResponse>();
                SqlCommand cmd = new SqlCommand("GetAllOrders", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", userId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderResponse order = new OrderResponse();
                        OrderResponse res;
                        res = ScanData(order, reader);
                        ordersResponse.Add(res);
                    }
                    sqlConnection.Close();
                    return ordersResponse;
                }
                else
                {
                    sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    

    public OrderResponse ScanData(OrderResponse order, SqlDataReader reader)
    {
        order.OrderId = Convert.ToInt32(reader["OrderId"] == DBNull.Value ? default : reader["OrderId"]);
        order.AddressId = Convert.ToInt32(reader["AddressId"] == DBNull.Value ? default : reader["AddressId"]);
        order.BookId = Convert.ToInt32(reader["BookId"] == DBNull.Value ? default : reader["BookId"]);
        order.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
        order.BookQuantity = Convert.ToInt32(reader["BookQuantity"] == DBNull.Value ? default : reader["BookQuantity"]);
        order.OrderDateTime = Convert.ToDateTime(reader["OrderDate"] == DBNull.Value ? default : reader["OrderDate"]);
        order.OrderDate = order.OrderDateTime.ToString("dd-MM-yyyy");
        order.TotalPrice = Convert.ToInt32(reader["TotalPrice"] == DBNull.Value ? default : reader["TotalPrice"]);
        order.BookName = Convert.ToString(reader["BookName"] == DBNull.Value ? default : reader["BookName"]);
        order.BookImage = Convert.ToString(reader["BookImage"] == DBNull.Value ? default : reader["BookImage"]);
        order.AuthorName = Convert.ToString(reader["AuthorName"] == DBNull.Value ? default : reader["AuthorName"]);

        return order;
    }
  }
}
