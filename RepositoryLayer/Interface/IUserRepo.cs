using CommanLayer.Model;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserRegisterModel UserRegistration(UserRegisterModel model);
        public object GetData();
        public LoginTokenModel LoginUser(Login model);
        public bool ResetPassword(string email, string password);
        public ForgotPasswordModel ForgotPassword(string email);
    }
}
