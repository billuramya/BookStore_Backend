using CommanLayer.Model;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressManager addressManager;
        public AddressController(IAddressManager addressManager)
        {
            this.addressManager = addressManager;
        }
        [HttpPost("AddUserAddress")]
        public IActionResult Add_Address(AddressModel model)
        {
            var address = addressManager.AddAddress(model);
            if (address != null)
            {
                return Ok(new { success = true, message = "Adrress added Successfully", Data = address });
            }
            else
            {
                return BadRequest(new { success = false, Message = "eeror" });
            }
        }


        [HttpGet("GetUserAddresses")]
        public IActionResult Get_addreses(int userid)
        {
            var address = addressManager.GetAddresses(userid);
            if (address != null)
            {
                return Ok(new { success = true, message = "Adrress added Successfully", Data = address });
            }
            else
            {
                return BadRequest(new { success = false, Message = "eeror" });
            }
        }

        [HttpPut]
        [Route("UpdateAddress")]

        public IActionResult update_address(UpdateAddressModel model)
        {
            var address = addressManager.UpdateAddress(model);
            if (address != null)
            {
                return Ok(new { success = true, message = "Adrress Updated Successfully", Data = address });
            }
            else
            {
                return BadRequest(new { success = false, Message = "eeror" });
            }
        }

    }
}
