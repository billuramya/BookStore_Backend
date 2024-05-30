using CommanLayer.Model;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewManager reviewManager;
        public ReviewController(IReviewManager reviewManager)
        {
            this.reviewManager = reviewManager;   
        }
        [HttpPost("AddReview")]
        public IActionResult Add_Review(ReviewModel model)
        {
            var review =reviewManager.AddReview(model);
            if (review != null)
            {
                return Ok(new { success = true, message = "Review Added Successfully", Data = review });
            }
            else
            {
                return BadRequest(new { success = false, message = "Something error" });
            }
        }

        [HttpGet("GetAllReviews")]

        public IActionResult Get_Reviews(int bookid)
        {
            var review = reviewManager.GetReviews(bookid);
            if (review != null)
            {
                return Ok(new { success = true, message = "Get The Review By Id Successfully", Data = review });
            }
            else
            {
                return BadRequest(new { success = false, message = "Something error" });
            }
        }

    }
}