using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.Entities;
namespace R2S.Training.Dao
{
    interface IProductDAO
    {
        List<Product> SearchAllProduct();

        Product SearchProductById(int product_id);
        bool AddProduct(Product product);

        bool UpdateProduct(Product product);
    }
}
