using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class AddressModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }
    }
}
