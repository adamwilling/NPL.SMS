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
            try
            {
                db.AddOrder(order);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOrderById(int order_id)
        {
            return db.DeleteOrderById(order_id);
        }

        public List<Order> GetAllOrdersByCustomerId(int cutomerId)
        {
            try
            {
                List<Order> listOrder= null;
                DataTable dt = db.GetAllOrderByCutomerId(cutomerId);
                if (dt.Rows.Count > 0)
                {
                    listOrder = new List<Order>();
                    foreach (DataRow row in dt.Rows)
                    {
                        int productId = int.Parse(row["order_id"].ToString());
                        string dateTime = row["order_date"].ToString();
                        int customerId = int.Parse(row["customer_id"].ToString());
                        int employee_id = int.Parse(row["product_id"].ToString());
                        double total = double.Parse(row["total"].ToString());
                        Order customer = new Order(productId,dateTime,customerId,employee_id,total);
                        listOrder.Add(customer);
                    }
                }
                return listOrder;
            }
            catch
            {
                return null;
            }
        }

        public Order GetOrderById(int order_id)
        {
            try
            {
                Order order = null;
                DataTable dt = db.SearchOrderById(order_id);
                if (dt.Rows.Count > 0)
                {
                        int productId = int.Parse(dt.Rows[0]["order_id"].ToString());
                        string dateTime = dt.Rows[0]["order_date"].ToString();
                        int customerId = int.Parse(dt.Rows[0]["customer_id"].ToString());
                        int employee_id = int.Parse(dt.Rows[0]["product_id"].ToString());
                        double total = double.Parse(dt.Rows[0]["total"].ToString());
                    order = new Order(productId, dateTime, customerId, employee_id, total);
                }
                return order;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateOrderTotal(int orderId)
        {
            try
            {
                db.UpdateOrderTotal(orderId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
