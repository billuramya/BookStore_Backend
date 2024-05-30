using CommanLayer.Model;
using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepo addressRepo;
        public AddressManager(IAddressRepo addressRepo)
        {
            this.addressRepo = addressRepo; 
        }
        public AddressModel AddAddress(AddressModel model)
        {
            return addressRepo.AddAddress(model);
        }
        public UpdateAddressModel UpdateAddress(UpdateAddressModel model)
        {
            return addressRepo.UpdateAddress(model);
        }
        public List<AddressModel> GetAddresses(int userid)
        {
            return addressRepo.GetAddresses(userid);
        }
    }
}
