using R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2S.Training.Dao
{
    interface ICustomerDAO
    {
         List<Customer> GetAllCustomer();

        Customer GetCustomerById(int customer_id);
        bool AddCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
        bool UpdateCustomer(Customer customer);
    }
}
