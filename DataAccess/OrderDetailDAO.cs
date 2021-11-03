using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
	class OrderDetailDAO
	{
		private static OrderDetailDAO instance;
		private static readonly object lockInstance = new object();

		public static OrderDetailDAO Instance { get
            {
				lock (lockInstance)
                {
					if (instance == null)
                    {
						instance = new OrderDetailDAO();
                    }
					return instance;
                }
            }
		}

		public void AddOrderDetail(OrderDetailObject orderDetail) {
			ApplicationDbContext db = new ApplicationDbContext();

			var duplicate = db.OrderDetailObject.SingleOrDefault(orderDtl =>
					(orderDtl.OrderId == orderDetail.OrderId) && (orderDtl.ProductId == orderDetail.ProductId));

			if (duplicate != null)
			{
				throw new Exception("Can not add 2 same OrderDetails");
			}

			var product = db.ProductObject.SingleOrDefault(prd => prd.ProductId == orderDetail.ProductId);

			if (product == null)
			{
				throw new Exception("Invalid Order Detail");
			}
			if (product.UnitsInStock < orderDetail.Quantity)
			{
				throw new Exception("OrderDetail's quantity must be less than or equal Product's Unit in stock");
			}

			product.UnitsInStock -= orderDetail.Quantity;
			db.ProductObject.Update(product);
			db.OrderDetailObject.Add(orderDetail);
			db.SaveChanges();
		}	

		public OrderDetailObject GetOrderDetail(int orderId, int productId)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			return db.OrderDetailObject.SingleOrDefault(o => o.OrderId == orderId && o.ProductId == productId);
		}

		public void Delete(int orderId, int productId)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			var orderDetail = db.OrderDetailObject.SingleOrDefault(o => (o.OrderId == orderId) && (o.ProductId == productId));

			if (orderDetail == null)
            {
				throw new Exception("Not found");
            }
			Delete(orderDetail);
		}

		public void Delete(OrderDetailObject orderDetail)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			if (orderDetail == null)
			{
				throw new Exception("Not found");
			}

			var product = db.ProductObject.SingleOrDefault(prd => prd.ProductId == orderDetail.ProductId);
			product.UnitsInStock += orderDetail.Quantity;
			db.ProductObject.Update(product);

			db.OrderDetailObject.Remove(orderDetail);
			db.SaveChanges();
		}

		public void DeleteByOrderId(int orderId)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			var orderDelete = db.OrderDetailObject.Where(o => o.OrderId == orderId).ToList();
			foreach (OrderDetailObject orderDetail in orderDelete)
			{
				Delete(orderDetail);
			}
		}

		public IEnumerable<OrderDetailObject> GetByOrderId(int orderId)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			return db.OrderDetailObject.Where(o => o.OrderId == orderId);
		}

		public void UpdateOrderDetail(OrderDetailObject orderDetail)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			var newProd = db.ProductObject.Find(orderDetail.ProductId);
			var oldOrderDetail = db.OrderDetailObject.Find(orderDetail.OrderId, orderDetail.ProductId);
			if (oldOrderDetail != null)
			{
				var oldProd = db.ProductObject.Find(oldOrderDetail.ProductId);
				orderDetail.UnitPrice = newProd.UnitPrice;
				newProd.UnitsInStock -= orderDetail.Quantity;
				oldProd.UnitsInStock += oldOrderDetail.Quantity;
				db.OrderDetailObject.Update(orderDetail);
				db.SaveChanges();
			}
		}

	}
}
