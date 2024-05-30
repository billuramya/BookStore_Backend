using CommanLayer.Model;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager bookManager;
        public BookController(IBookManager bookManager)
        {
            this.bookManager = bookManager;   
        }
        //[Authorize(Roles =Role.Admin)]
        [HttpPost]
        [Route("BookRegister")]
        public IActionResult Register_Book(BookModel model)
        {
            var registerdata = bookManager.BookRegistration(model);
            if (registerdata != null)
            {
                return Ok(new { success = true, Message = "Book Registration Successful", Data = registerdata });
            }
            return BadRequest(new { success = false, Message = "Book Registration failed" });
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpGet("GetAllBooks")]
        public IActionResult GetBooks()
        {
            var books = bookManager.GetAllBooks();
            if (books != null)
            {
                return Ok(new { success = true, Message = "All books are fetched", Data = books });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Something error!" });
            }
        }

       
        //[Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateById")]
        public IActionResult Update(BookModel model, int BookId)
        {
            var books = bookManager.UpdateBook(model,BookId);
            if (books != null)
            {
                return Ok(new { success = true, Message = "Update Book Successfuly", Data = books });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Updation Failed" });
            }
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteById")]
        public IActionResult Delete(int BookId)
        {
            var res= bookManager.DeleteBook(BookId);
            if(res != null)
            {
                return Ok( new { success = true, Message = "Delete Book Successfuly", Data = res });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Delete Book Failed" });
            }
        }

       // [Authorize(Roles = Role.Admin)]
        [HttpGet("GetBookById")]
        public IActionResult GeBookBy(int BookId)
        {
            var books = bookManager.GetBookById(BookId);
            if (books != null)
            {
                return Ok(new { success = true, Message = " book are fetched", Data = books });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Something error!" });
            }
        }


        [HttpGet("GetBookByName")]
        public IActionResult BookByName(string title)
        {
            var books = bookManager.GetBookByName(title);
            if (books != null)
            {
                return Ok(new { success = true, Message = "All books are fetched", Data = books });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Something error!" });
            }
        }

        [HttpGet("totalcount")]
        public IActionResult GetTotalBook(int UserId)
        {
            var totalCount = bookManager.GetTotalBookCount(UserId);
            if (totalCount != null)
            {
                return Ok(new { success = true, Message = "Count books Sucssecfuly", Data = totalCount });
            }
            else
            {

                return BadRequest(new { success = false, Message = "Failed to get total book count." });
            }
            
        }
    }
}
