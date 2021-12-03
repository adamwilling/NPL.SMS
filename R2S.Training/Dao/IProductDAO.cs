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
        Product SearchProductById(int product_id);
    }
}
