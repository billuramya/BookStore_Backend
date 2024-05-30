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
    public class CartManager:ICartManager
    {
        private readonly ICartRepo cartRepo;
        public CartManager(ICartRepo cartRepo)
        {
            this.cartRepo = cartRepo;
        }
        public List<BookEntity> AddToCart(CartModel model, int userid)
        {
            return cartRepo.AddToCart(model, userid);
        }
        public List<BookEntity> GetCartBooks(int userid)
        {
            return cartRepo.GetCartBooks(userid);
        }
        public bool DeleteCart(DeleteCart model)
        {
            return cartRepo.DeleteCart(model);
        }
        public CartModel UpdateQuantity(int userid, CartModel model)
        {
            return cartRepo.UpdateQuantity(userid, model);
        }
    }
}
