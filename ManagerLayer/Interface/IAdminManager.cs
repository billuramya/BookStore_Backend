using CommanLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IAdminManager
    {
        public AdminModel AddAdmin(AdminModel model);
        public AdminTokenModel LoginAdmin(AdminLoginModel model);
    }
}
