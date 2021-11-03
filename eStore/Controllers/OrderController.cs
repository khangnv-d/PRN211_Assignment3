using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using eStore.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;



namespace eStore.Controllers
{
    public class OrderController : Controller
    {
        
        private IOrderRepository orderRepository = new OrderRepository();
        private IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();

        public OrderController()
        {
            
        }

        public IActionResult Index()
        {
            var data = orderRepository.GetOrderList();
            return View(data);
        }

        public IActionResult ViewOrder()
        {
            IEnumerable<OrderObject> orderList = orderRepository.GetOrderList();
            return View(orderList);
        }

        public IActionResult CreateOrder()
        {
            return View();
        }

        public IActionResult Create (OrderObject orderObject)
        {
            OrderDetailObject orderItem = new OrderDetailObject();
            var cartList = HttpContext.Session.GetObject<List<CartObject>>("cart");
            if (cartList == null)
            {
                return View("ViewOrder");
            }

            orderRepository.AddOrder(orderObject);
            foreach (CartObject item in cartList)
            {
                orderItem.OrderId = orderObject.OrderId;
                orderItem.ProductId = item.ProductId;
                orderItem.UnitPrice = item.UnitPrice;
                orderItem.Quantity = item.Quantity;
                orderItem.Discount = item.Discount;
                orderDetailRepository.Add(orderItem);
            }
            return View("ViewOrder", orderRepository.GetOrderList());
        }

        public ActionResult Delete(int id)
        {
            orderRepository.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult SearchById (string? orderId)
        {
            try
            {
                var result = new List<OrderObject>();
                var intOrderId = Int32.Parse(orderId);
                OrderObject targetOrder = orderRepository.GetOrderById(intOrderId);
                if (targetOrder != null)
                {
                    result.Add(targetOrder);
                }
                return View("ViewOrder", result);

            } catch (FormatException ex)
            {
                return View("ErrorAction", "Error");

            } catch (Exception ex)
            {
                return View("ErrorAction", ex.Message);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                try
                {
                    orderRepository.DeleteOrder((int)id);
                    orderDetailRepository.DeleteByOrderId((int) id);
                    return View("ViewOrder", orderRepository.GetOrderList());
                } catch (Exception ex)
                {
                    return View("ErrorAction", "Error");
                }

            } else
            {
                return View("ErrorAction", "Not found");
            }
        }

    }
}
