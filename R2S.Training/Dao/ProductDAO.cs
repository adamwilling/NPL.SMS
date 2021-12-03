using R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.ConnectionManager;
using System.Data;
namespace R2S.Training.Dao
{
    class ProductDAO : IProductDAO
    {
        ConnectionManagers db = null;
        public ProductDAO()
        {
            db = new ConnectionManagers();
        }

        public Product SearchProductById(int productId)
        {
            return db.SearchProductById(productId);
        }
    }
}
