using CommanLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IOrderManager
    {
        public List<BookEntity> AddToOrder(OrderModel model, int userid);
        public List<BookEntity> GetOrders(int userid);
        public double GetPriceInOrder(int userid);
    }
}
