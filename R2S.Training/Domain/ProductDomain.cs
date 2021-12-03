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

        internal Product SearchProductById(int productId)
        {
            return productDao.SearchProductById(productId);
        }
    }
}
