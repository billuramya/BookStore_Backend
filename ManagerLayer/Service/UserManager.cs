using CommanLayer.Model;
using CommonLayer.Models;
using ManagerLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
   public class UserManager: IUserManager
    {
        private readonly IUserRepo userRepo;
        public UserManager(IUserRepo userRepo)
        { 
            this.userRepo = userRepo;
            
        }
        public UserRegisterModel UserRegistration(UserRegisterModel model)
        {
            return userRepo.UserRegistration(model);
        }
        public object GetData()
        {
            return  userRepo.GetData();
        }
        public LoginTokenModel LoginUser(Login model)
        {
            return userRepo.LoginUser(model);
        }
        public bool ResetPassword(string email, string password)
        {
            return userRepo.ResetPassword(email, password);
        }
        public ForgotPasswordModel ForgotPassword(string email)
        {
            return userRepo.ForgotPassword(email);
        }
    }
}
