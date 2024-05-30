using CommanLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddressRepo
    {
        public AddressModel AddAddress(AddressModel model);
        public UpdateAddressModel UpdateAddress(UpdateAddressModel model);
        public List<AddressModel> GetAddresses(int userid);
    }
}
