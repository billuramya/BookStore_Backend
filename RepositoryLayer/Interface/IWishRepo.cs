﻿using CommanLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IWishRepo
    {
        public List<BookEntity> AddToWishList(WishList model);
        public List<BookEntity> GetWhishListBooks(int userid);
        public bool DeleteWhishlist(WishList model);
    }
}