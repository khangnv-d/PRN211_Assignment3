using System;
using System.Collections.Generic;
using BusinessObject;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<OrderObject> GetOrderList();

        IEnumerable<OrderObject> GetOrderListByMemberId(int memberId);

        void AddOrder(OrderObject orderObject);

        void DeleteOrder(int orderId);

        OrderObject GetOrderById(int orderId);
    }
}
