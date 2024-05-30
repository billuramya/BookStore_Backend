using CommanLayer.Model;
using ManagerLayer.Interface;
using ManagerLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminManager adminManager;
        public AdminController(IAdminManager adminManager)
        {
            this.adminManager = adminManager;
        }

        [HttpPost]
        [Route("AdminRegister")]
        public IActionResult Register_Admin(AdminModel model)
        {
            var registerdata = adminManager.AddAdmin(model);
            if (registerdata != null)
            {
                return Ok(new { success = true, Message = "Admin Registration Successful", Data = registerdata });
            }
            return BadRequest(new { success = false, Message = "Admin Registration failed" });
        }



        [HttpPost]
        [Route("Adimlogin")]
        public IActionResult UserLogin(AdminLoginModel login)
        {
            var res = adminManager.LoginAdmin(login);
            if (res != null)
            {
                return Ok(new { Success = true, Message = "Admin login  Success!", Data = res });
            }
            else
            {
                return BadRequest(new { Success = false, Message = "Admin Login Failed", Data = res });
            }
        }
    }
}
