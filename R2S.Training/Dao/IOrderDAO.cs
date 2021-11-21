using R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2S.Training.Dao
{
    interface IOrderDAO
    {
        List<Order> GetAllOrdersByCustomerId(int cutomerId);

        bool AddOrder(Order order);

        bool UpdateOrderTotal(int orderId);

        Order SearchOrderById(int orderId);

        bool DeleteOrderById(int order_id);
    }
}
