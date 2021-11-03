using BusinessObject;
using System;
using System.Collections.Generic;


namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
		public void Add(OrderDetailObject detail)
		{
			OrderDetailDAO.Instance.AddOrderDetail(new OrderDetailObject
			{
				Discount = detail.Discount,
				OrderId = detail.OrderId,
				ProductId = detail.ProductId,
				Quantity = detail.Quantity,
				UnitPrice = detail.UnitPrice
			});
		}

		public void AddRange(IEnumerable<OrderDetailObject> orderDetails)
		{
			foreach (var orderDtl in orderDetails)
			{
				OrderDetailDAO.Instance.AddOrderDetail(orderDtl);
			}
		}

		public void Delete(int orderId, int productId) => OrderDetailDAO.Instance.Delete(orderId, productId);

		public void DeleteByOrderId(int orderId) => OrderDetailDAO.Instance.DeleteByOrderId(orderId);

		public OrderDetailObject Get(int orderId, int productId) => OrderDetailDAO.Instance.GetOrderDetail(orderId, productId);

		public IEnumerable<OrderDetailObject> GetByOrderId(int orderId)
			=> OrderDetailDAO.Instance.GetByOrderId(orderId);

        public void Update(OrderDetailObject orderDetail)
		{
			OrderDetailDAO.Instance.UpdateOrderDetail(new OrderDetailObject
			{
				OrderId = orderDetail.OrderId,
				Discount = orderDetail.Discount,
				ProductId = orderDetail.ProductId,
				Quantity = orderDetail.Quantity,
				UnitPrice = orderDetail.UnitPrice
			});
		}
	}
}
