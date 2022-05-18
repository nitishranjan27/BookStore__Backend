using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IOrderRL
    {
        public OrderModel AddOrder(OrderModel orderModel, int userId);
        public List<OrderResponse> GetAllOrder(int userId);
    }
}
