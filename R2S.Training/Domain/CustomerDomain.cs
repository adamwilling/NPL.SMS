using R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.Dao;
namespace R2S.Training.Domain
{
    class CustomerDomain
    {
        CustomerDAO customerDao = null;
        public CustomerDomain()
        {
            customerDao = new CustomerDAO();
        }
        internal List<Customer> GetAllCustomer()
        {
            return customerDao.GetAllCustomer();
        }

        internal bool AddCustomer(Customer customer)
        {
            return customerDao.AddCustomer(customer);
        }

        internal bool DeleteCustomer(int customerId)
        {
            return customerDao.DeleteCustomer(customerId);
        }

        internal bool UpdateCustomer(Customer customer)
        {
            if (customerDao.SearchCustomerById(customer.CustomerId) == null)
            {
                return false;
            }
            return customerDao.UpdateCustomer(customer);
        }
    }
}
