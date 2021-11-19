﻿using R2S.Training.Entities;
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
            try
            {
                db.AddCustmer(customer);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                db.DeleteCustmer(customerId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Customer> GetAllCustomer()
        {
            try
            {
                List<Customer> listCustomer = null;
                DataTable dt = db.GetAllCustomer();
                if (dt.Rows.Count > 0)
                {
                    listCustomer = new List<Customer>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Customer customer = new Customer(int.Parse(row["customer_id"].ToString()), row["customer_name"].ToString());
                        listCustomer.Add(customer);
                    }
                }
                return listCustomer;
            }
            catch
            {
                return null;
            }
        }

        public Customer GetCustomerById(int customer_id)
        {
            try
            {
                Customer customer = null;
                DataTable dt = db.GetAllCustomer();
                if (dt.Rows.Count > 0)
                {
                    customer = new Customer(int.Parse(dt.Rows[0]["customer_id"].ToString()), dt.Rows[0]["customer_name"].ToString());

                }
                return customer;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                db.UpdateCustmer(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
