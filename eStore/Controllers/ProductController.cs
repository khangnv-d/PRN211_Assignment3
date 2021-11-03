using System;
using System.Collections.Generic;
using System.Linq;
using eStore.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository prdRepository = null;
        IOrderDetailRepository orddetailRepository = null;
        public ProductController()
        {
            prdRepository = new ProductRepository();
            orddetailRepository = new OrderDetailRepository();
        }

       public IActionResult Index(IEnumerable<BusinessObject.ProductObject> prdList)
       {
            if(prdList.Count() == 0)
            {
                prdList = prdRepository.GetAllProducts();
            }
            ViewData["Role"] = HttpContext.Session.GetObject<String>("Role");
            return View(prdList);
       }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult SearchByProductName(string prdName)
        {
            var prdList = prdRepository.GetProductListByName(prdName);
            return View("Index", prdList);
        }
        public ActionResult SearchByProductPriceRange(decimal price01, decimal price02)
        {
            var prdList = prdRepository.GetProductListByPriceRange(price01, price02);
            return View("Index", prdList);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductObject prd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    prdRepository.AddNewProduct(prd);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(prd);
            }
        }

        // GET
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prd = prdRepository.GetProductByID(id.Value);
            if (prd == null)
            {
                return NotFound();
            }
            return View(prd);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, ProductObject prd)
        {
            try
            {
                if (id != prd.ProductId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    prdRepository.UpdateProduct(prd);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prd = prdRepository.GetProductByID(id.Value);
            if (prd == null)
            {
                return NotFound();
            }
            return View(prd);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                prdRepository.DeleteAProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        public ActionResult AddToCart(string unitPrice, string productId, string quantity, string discount)
        {
            var prdList = prdRepository.GetAllProducts();
            var cartList = HttpContext.Session.GetObject<List<CartObject>>("cart");
            if (cartList == null)
            {
                cartList = new List<CartObject>();
            }
            if(cartList.Count == 0)
            {
                cartList.Add(new CartObject
                {
                    ProductId = int.Parse(productId),
                    UnitPrice = decimal.Parse(unitPrice),
                    Quantity = int.Parse(quantity),
                    Discount = float.Parse(discount)
                });
            }
            else
            {
                foreach (CartObject item in cartList)
                {
                    if (item.ProductId == int.Parse(productId))
                    {
                        item.Quantity += int.Parse(quantity);
                        item.Discount = float.Parse(discount);
                        HttpContext.Session.SetObject("cart", cartList);
                        return View("Index", prdList);
                    }
                }
                cartList.Add(new CartObject
                {
                    ProductId = int.Parse(productId),
                    UnitPrice = decimal.Parse(unitPrice),
                    Quantity = int.Parse(quantity),
                    Discount = float.Parse(discount)
                });
            }

            HttpContext.Session.SetObject("cart", cartList);
            return View("Index", prdList);
        }
    }
}
