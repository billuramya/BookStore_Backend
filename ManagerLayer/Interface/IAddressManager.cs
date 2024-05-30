using CommanLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IAddressManager
    {
        public AddressModel AddAddress(AddressModel model);
        public UpdateAddressModel UpdateAddress(UpdateAddressModel model);
        public List<AddressModel> GetAddresses(int userid);
    }
}
