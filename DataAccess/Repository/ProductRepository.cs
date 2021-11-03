using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<ProductObject> GetAllProducts() => ProductDAO.Instance.GetAllProducts();
        public void AddNewProduct(ProductObject prd) => ProductDAO.Instance.AddNewProduct(prd);
        public IEnumerable<ProductObject> GetProductListByName(string prdName) 
            => ProductDAO.Instance.GetProductListByName(prdName);
        public void DeleteAProduct(int productId) => ProductDAO.Instance.DeleteProduct(productId);
        public ProductObject GetProductByID(int productId) => ProductDAO.Instance.GetProductByID(productId);
        public IEnumerable<ProductObject> GetProductListByPriceRange(decimal price1, decimal price2) 
            => ProductDAO.Instance.GetProductListByPriceRange(price1, price2);
        public void UpdateProduct(ProductObject prd) => ProductDAO.Instance.UpdateProduct(prd);
       
    }
}
