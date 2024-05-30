using CommanLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IBookRepo
    {
        public BookModel BookRegistration(BookModel model);
        public object GetAllBooks();
       
        public bool UpdateBook(BookModel model, int BookId);
        public bool DeleteBook(int BookId);
        public BookEntity GetBookById(int BookId);

        public BookEntity GetBookByName(string Title);

        public List<BookCountEntity> GetTotalBookCount(int UserId);
    }
}
