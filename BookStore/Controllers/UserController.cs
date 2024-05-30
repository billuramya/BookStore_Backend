using CommanLayer.Model;
using CommonLayer.Models;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register_User(UserRegisterModel model)
        {
            var registerdata = manager.UserRegistration(model);
            if (registerdata != null)
            {
                return Ok(new { success = true, Message = "User Registration Successful", Data = registerdata });
            }
            return BadRequest(new { success = false, Message = "Registration failed" });
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = manager.GetData();
            if (users != null)
            {
                return Ok(new { Success = true, Message = "All users found", Data = users });
            }
            else
            {
                return BadRequest(new { success = false, Message = "users not found" });
            }
        }



        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin(Login login)
        {
            var res = manager.LoginUser(login);
            if (res != null)
            {
                return Ok(new { Success = true, Message = "login  Success!", Data = res });
            }
            else
            {
                return BadRequest(new { Success = false, Message = "Login Failed", Data = res });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult reset_password(string password)
        {
            var userid = User.Claims.Where(x => x.Type == "Email").FirstOrDefault().Value;

            var data = manager.ResetPassword(userid, password);
            if (data != null)
            {
                return Ok(new { Success = true, Message = "Password Changed Sucessfully" });
            }
            return BadRequest(new { Success = false, Message = "Password Changed UnSucessfully" });

        }


        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var password = manager.ForgotPassword(email);
            if (password != null)
            {
                Send send = new Send();
                send.SendMail(password.Email, "Password is Trying to Changed is that you....!\nToken: " + password.token);
                Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");



                return Ok(new { Success = true, Message = "forgotpassword Changed Sucessfully", Data = password.token });

            }
            return BadRequest(new { Success = false, Message = "forgotpassword Changed UnSucessfully" });

        }
    }
}
