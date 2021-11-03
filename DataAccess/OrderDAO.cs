using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess
{
    class OrderDAO
    {
        private static OrderDAO instance;
        private static readonly object instanceLock = new object();
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }

                    return instance;
                }
            }
        }

        public OrderObject GetOrderById(int orderId)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var orderObject = dbContext.OrderObject.SingleOrDefault(order => order.OrderId == orderId);
            return orderObject;
        }

        public void AddOrder(OrderObject orderObject)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.OrderObject.Add(orderObject);
            db.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var orderObject = dbContext.OrderObject.SingleOrDefault(orderObject => orderObject.OrderId == orderId);
            if (orderObject != null)
            {
                dbContext.OrderObject.Remove(orderObject);
                dbContext.SaveChanges();
            } else
            {
                throw new Exception("Not found");
            }
        }

        public IEnumerable<OrderObject> GetOrderList ()
        {
            List<OrderObject> orderList;

            ApplicationDbContext db = new ApplicationDbContext();
            orderList = db.OrderObject.ToList();
            return orderList;
        }

        public IEnumerable<OrderObject> GetOrderListByMemberId(int memId)
        {
            IEnumerable<OrderObject> orderList;
            ApplicationDbContext db = new ApplicationDbContext();
            orderList = db.OrderObject.Where(order => order.MemberId == memId);
            return orderList;
        }

    }
}
