using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductObject> GetAllProducts();
        void AddNewProduct(ProductObject prd);
        void DeleteAProduct(int productId);
        ProductObject GetProductByID(int productId);
        void UpdateProduct(ProductObject prd);
        IEnumerable<ProductObject> GetProductListByName(string productName);
        IEnumerable<ProductObject> GetProductListByPriceRange(decimal price01, decimal price02);
    }
}
