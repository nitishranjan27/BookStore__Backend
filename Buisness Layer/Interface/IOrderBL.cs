using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IOrderBL
    {
        public OrderModel AddOrder(OrderModel orderModel, int userId);
        public List<OrderResponse> GetAllOrder(int userId);
    }
}
