using CommanLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface ICartManager
    {
        public List<BookEntity> AddToCart(CartModel model, int userid);
        public List<BookEntity> GetCartBooks(int userid);
        public bool DeleteCart(DeleteCart model);
        public CartModel UpdateQuantity(int userid, CartModel model);
    }
}
