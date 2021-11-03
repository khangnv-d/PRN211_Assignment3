using BusinessObject;
using System;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
		void Add(OrderDetailObject detail);

		void AddRange(IEnumerable<OrderDetailObject> orderDetails);

		void Update(OrderDetailObject orderDetail);

		OrderDetailObject Get(int orderId, int productId);

		IEnumerable<OrderDetailObject> GetByOrderId(int orderId);

		void Delete(int orderId, int productId);

		void DeleteByOrderId(int orderId);
	}
}
