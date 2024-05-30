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
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepo orderRepo;
        public OrderManager(IOrderRepo orderRepo)
        {
            this.orderRepo = orderRepo;
        }
        public List<BookEntity> AddToOrder(OrderModel model, int userid)
        {
            return orderRepo.AddToOrder(model, userid);
        }
        public List<BookEntity> GetOrders(int userid)
        {
            return orderRepo.GetOrders(userid);
        }
        public double GetPriceInOrder(int userid)
        {
            return orderRepo.GetPriceInOrder(userid);   
        }
    }
}
