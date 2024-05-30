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
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepo admin;
        public AdminManager(IAdminRepo admin)
        {
            this.admin = admin;
        }
        public AdminModel AddAdmin(AdminModel model)
        {
            return admin.AddAdmin(model);
        }
        public AdminTokenModel LoginAdmin(AdminLoginModel model)
        {
            return admin.LoginAdmin(model);
        }
    }
}
