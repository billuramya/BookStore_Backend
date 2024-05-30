using CommanLayer.Model;
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
   public class BookManager :IBookManager
    {
        private readonly IBookRepo bookRepo;
        public BookManager(IBookRepo bookRepo)
        {
            this.bookRepo = bookRepo;
           
        }
        public BookModel BookRegistration(BookModel model)
        {
            return bookRepo.BookRegistration(model);
        }
        public object GetAllBooks()
        {
            return bookRepo.GetAllBooks();
        }
        
        public bool UpdateBook(BookModel model, int BookId)
        {
            return bookRepo.UpdateBook(model,BookId);  
        }
        public bool DeleteBook(int BookId)
        {
            return bookRepo.DeleteBook(BookId);
        }
        public BookEntity GetBookById(int BookId)
        {
            return bookRepo.GetBookById(BookId);
        }
        public BookEntity GetBookByName(string Title)
        {
            return bookRepo.GetBookByName(Title);
        }
        public List<BookCountEntity> GetTotalBookCount(int UserId)
        {
            return bookRepo.GetTotalBookCount(UserId);
        }
    }
}
