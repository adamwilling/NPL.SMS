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
                Console.WriteLine("*** Customer id not found!!!");
                return null;
            }
            return orderDao.GetAllOrdersByCustomerId(customerId);
        }

        internal bool AddOrder(Order order)
        {
            if(customerDAO.SearchCustomerById(order.CustomerId) == null)
            {
                Console.WriteLine("*** Customer id not found!!!");
                if (employeeDAO.SearchEmployeeById(order.EmployeeId) == null)
                {
                    Console.WriteLine("*** Employee id not found!!!");
                    return false;
                }
                return false;
            }
            return orderDao.AddOrder(order);
        }

        public double ComputeOrderTotal(int orderId)
        {
            if (orderDao.SearchOrderById(orderId) == null)
            {
                Console.WriteLine("*** Order id not found!!!");
                return 0;
            }
            return orderDao.ComputeOrderTotal(orderId);
        }

        internal bool UpdateOrderTotal(int orderId)
        {
            if (orderDao.SearchOrderById(orderId) == null)
            {
                Console.WriteLine("*** Order id not found!!!");
                return false;
            }

            return orderDao.UpdateOrderTotal(orderId);
        }
    }
}
