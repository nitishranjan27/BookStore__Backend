using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IAddressBL
    {
        public string AddAddress(AddressModel addressModel, int user_Id);
        public AddressModel UpdateAddress(AddressModel addressModel, int address_Id, int user_Id);
        public bool DeleteAddress(int address_Id, int user_Id);
        public List<AddressModel> GetAllAddresses(int user_Id);
    }
}
