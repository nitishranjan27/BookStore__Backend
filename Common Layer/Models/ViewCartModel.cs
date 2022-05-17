using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class ViewCartModel
    {
        public int cartId { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public int orderQuantity { get; set; }
        public BookModel Bookmodel { get; set; }
    }
}
