using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<ProductObject> GetAllProducts()
        {
            var prdList = new List<ProductObject>();
            try
            {
                using var context = new ApplicationDbContext();
                prdList = context.ProductObject.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return prdList;
        }
        public ProductObject GetProductByID(int productId)
        {
            ProductObject prd = null;
            try
            {
                using var context = new ApplicationDbContext();
                prd = context.ProductObject.SingleOrDefault(prd => prd.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return prd;
        }

        public IEnumerable<ProductObject> GetProductListByName(string productName)
        {
            IEnumerable<ProductObject> prdList = null;
            try
            {
                using var context = new ApplicationDbContext();

                if (productName is null || productName == "")
                {
                    prdList = context.ProductObject.ToList();
                }
                else
                {
                    prdList = context.ProductObject.Where(prd => prd.ProductName.ToUpper().Contains(productName.ToUpper())).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return prdList;
        }
        public IEnumerable<ProductObject> GetProductListByPriceRange(decimal price01, decimal price02)
        {
            IEnumerable<ProductObject> prdList = null;
            try
            {
                using var context = new ApplicationDbContext();
                prdList = context.ProductObject.Where(prd => (prd.UnitPrice >= price01) && (prd.UnitPrice <= price02)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return prdList;
        }

        public void AddNewProduct(ProductObject prd)
        {
            try
            {
                using var context = new ApplicationDbContext();
                context.ProductObject.Add(prd);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateProduct(ProductObject prd)
        {
            try
            {
                ProductObject product = GetProductByID(prd.ProductId);
                if (product != null)
                {
                    using var context = new ApplicationDbContext();
                    context.ProductObject.Update(prd);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No Existed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                ProductObject product = GetProductByID(productId);
                if (product != null)
                {
                    using var context = new ApplicationDbContext();
                    context.ProductObject.Remove(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No Existed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
