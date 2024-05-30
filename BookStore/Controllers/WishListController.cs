using CommanLayer.Model;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishManager wishManager;
        public WishListController(IWishManager wishManager)
        {
            this.wishManager = wishManager;
        }
        [HttpPost]
        [Route("AddToWishList")]

        public IActionResult Add_whishlist(WishList model)
        {
            var data = wishManager.AddToWishList(model);

            if (data == null)
            {
                return BadRequest(new { success = false, message = "Someting error" });
            }
            return Ok(new { Success = true, mesaage = "Added to WishList", Data = data });
        }

        [HttpGet]
        [Route("GetWhishList")]

        public IActionResult get_wishlist(int userid)
        {
            var data = wishManager.GetWhishListBooks(userid);

            if (data == null)
            {
                return BadRequest(new { success = false, message = "Someting error" });
            }

            return Ok(new { Success = true, mesaage = "WishList items are", Data = data });
        }

        [HttpPost]
        [Route("DeleteWhishList")]

        public IActionResult delete_whishlist(WishList model)
        {
            var data =wishManager.DeleteWhishlist(model);

            if (data == null)
            {
                return BadRequest(new { success = false, message = "Someting error" });
            }
            return Ok(new { message = "deleted sucessfully", result = true });
        }
    }
}
    