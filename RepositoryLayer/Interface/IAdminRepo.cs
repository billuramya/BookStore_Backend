using CommanLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAdminRepo
    {
        public AdminModel AddAdmin(AdminModel model);
        public AdminTokenModel LoginAdmin(AdminLoginModel model);
    }
}
