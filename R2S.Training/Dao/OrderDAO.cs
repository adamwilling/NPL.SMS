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
    class OrderDAO : IOrderDAO
    {
        ConnectionManagers db = null;
        public OrderDAO()
        {
            db = new ConnectionManagers();
        }
        public bool AddOrder(Order order)
        {
            return db.AddOrder(order);
        }

        public bool DeleteOrderById(int orderId)
        {
            return db.DeleteOrderById(orderId);
        }

        public List<Order> GetAllOrdersByCustomerId(int cutomerId)
        {
            return db.GetAllOrderByCutomerId(cutomerId);
        }

        public Order SearchOrderById(int orderId)
        {
            return db.SearchOrderById(orderId);
        }

        public double ComputeOrderTotal(int orderId)
        {
            return db.ComputeOrderTotal(orderId);
        }

        public bool UpdateOrderTotal(int orderId)
        {
            return db.UpdateOrderTotal(orderId);
        }
    }
}
