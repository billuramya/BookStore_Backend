using CommanLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IOrderRepo
    {
        public List<BookEntity> AddToOrder(OrderModel model, int userid);
        public List<BookEntity> GetOrders(int userid);
        public double GetPriceInOrder(int userid);
    }
}
