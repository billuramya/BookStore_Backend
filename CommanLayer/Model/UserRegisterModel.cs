using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanLayer.Model
{
    public class UserRegisterModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }

    }
}
