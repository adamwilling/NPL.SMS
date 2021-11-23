using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.Dao;
using R2S.Training.Entities;

namespace R2S.Training.Domain
{
    class ProductDomain
    {
        ProductDAO productDao = null;
        public ProductDomain()
        {
            productDao = new ProductDAO();
        }
        internal List<Product> GetAllProduct()
        {
            return productDao.GetAllProduct();
        }

        internal Product SearchProductById(int productId)
        {
            return productDao.SearchProductById(productId);
        }

        internal bool AddProduct(Product product)
        {
            return productDao.AddProduct(product);
        }

        internal bool DeleteProduct(int productId)
        {
            if (productDao.SearchProductById(productId) == null)
            {
                Console.WriteLine("***Mã nhân viên không tồn tại!!!");
                return false;
            }
            return productDao.DeleteProduct(productId);
        }

        internal bool UpdateProduct(Product product)
        {
            if (productDao.SearchProductById(product.ProductId) == null)
            {
                return false;
            }
            return productDao.UpdateProduct(product);
        }
    }
}
