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
    public class WishManager : IWishManager
    {
        private readonly IWishRepo wishRepo;
        public WishManager(IWishRepo wishRepo)
        {
            this.wishRepo = wishRepo;
        }
        public List<BookEntity> AddToWishList(WishList model)
        {
            return wishRepo.AddToWishList(model);
        }
        public List<BookEntity> GetWhishListBooks(int userid)
        {
            return wishRepo.GetWhishListBooks(userid);
        }
        public bool DeleteWhishlist(WishList model)
        {
            return wishRepo.DeleteWhishlist(model);
        }
    }
}
