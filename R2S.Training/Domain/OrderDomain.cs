using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.Dao;
using R2S.Training.Entities;
namespace R2S.Training.Domain
{
    class OrderDomain
    {
        OrderDAO orderDao = null;
        CustomerDAO customerDAO = null;
        EmployeeDAO employeeDAO = null;
        public OrderDomain()
        {
            orderDao = new OrderDAO();
            customerDAO = new CustomerDAO();
            employeeDAO = new EmployeeDAO();
        }

        public List<Order> GetAllOrdersByCustomerId(int customerId)
        {
            if (customerDAO.SearchCustomerById(customerId) == null)
            {
                Console.WriteLine("*Mã khách hàng không tồn tại!!!");
                return null;
            }
            return orderDao.GetAllOrdersByCustomerId(customerId);
        }

        internal bool AddOrder(Order order)
        {
            if(customerDAO.SearchCustomerById(order.CustomerId) == null)       // Kiểm tra mã khách hàng có tồn tại hay không
            {
                Console.WriteLine("*Mã khách hàng không tồn tại!!!");
                if (employeeDAO.SearchEmployeeById(order.EmployeeId) == null)      // Kiểm tra mã nhân viên có tồn tại hay không
                {
                    Console.WriteLine("*Mã nhân viên không tồn tại!!!");
                    return false;
                }
                return false;
            }
            return orderDao.AddOrder(order);
        }

        internal bool UpdateOrderTotal(int orderId)
        {
            if (orderDao.SearchOrderById(orderId) == null)      // Kiểm tra mã đơn hàng có tồn tại hay không 
            {
                return false;
            }

            return orderDao.UpdateOrderTotal(orderId);
        }

    }
}
