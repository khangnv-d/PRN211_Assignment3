using System;
using System.Collections.Generic;
using BusinessObject;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        {
        }

        public void AddOrder(OrderObject orderObject) => OrderDAO.Instance.AddOrder(orderObject);

        public void DeleteOrder(int orderId) => OrderDAO.Instance.DeleteOrder(orderId);

        public OrderObject GetOrderById(int orderId) => OrderDAO.Instance.GetOrderById(orderId);

        public IEnumerable<OrderObject> GetOrderList() => OrderDAO.Instance.GetOrderList();

        public IEnumerable<OrderObject> GetOrderListByMemberId(int memberId) 
                => OrderDAO.Instance.GetOrderListByMemberId(memberId);

    }
}
