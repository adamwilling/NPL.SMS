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
    class CustomerDAO : ICustomerDAO
    {
        ConnectionManagers db = null;
        public CustomerDAO()
        {
            db = new ConnectionManagers();
        }
        public bool AddCustomer(Customer customer)
        {
            return db.AddCustomer(customer);
        }

        public bool DeleteCustomer(int customerId)
        {
            return db.DeleteCustmer(customerId);
        }

        public List<Customer> GetAllCustomer()
        {
            return db.GetAllCustomer();
        }

        public Customer SearchCustomerById(int customerId)
        {
            return db.SearchCustomerById(customerId);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return db.UpdateCustmer(customer);
        }
    }
}
