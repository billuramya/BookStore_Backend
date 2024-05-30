using CommanLayer.Model;
using ManagerLayer.Interface;
using ManagerLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartManager cartManager;
        public CartController(ICartManager cartManager)
        {
            this.cartManager = cartManager;
        }
        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddCart(int userid, CartModel model)
        {
            var data = cartManager.AddToCart(model, userid);
            if (data == null)
            {
                return NotFound(new { success = false, message = "something error" });
            }
            return Ok(new { success = true, message = "Added to Cart Successfully", Data = data });
        }

        [HttpGet]
        [Route("GetCard")]
        public IActionResult GetCard(int userid)
        {
            var data = cartManager.GetCartBooks(userid);
            if (data == null)
            {
                return BadRequest(new { success = false, message = "something error" });
            }
            return Ok(data);
        }


        [HttpPost]
        [Route("DeleteCart")]

        public IActionResult Delete_cart(DeleteCart model)
        {
            var data = cartManager.DeleteCart(model);
            if (!data)
            {
                return BadRequest("Cart Not found");
            }
            return Ok(new { message = "deleted sucessfully", result = true });
        }


        [HttpPut]
        [Route("UpdateQuantity")]
        public IActionResult UpdateQty(int userid, CartModel model)
        {
            var data = cartManager.UpdateQuantity(userid, model);
            if (data == null)
            {
                return BadRequest(new { success = false, message = "something error" });
            }
            return Ok(new { success = true, message = "Quantity updated", Data = data });
        }

    }
}

