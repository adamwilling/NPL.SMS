using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2S.Training.Entities
{
    class Customer
    {
        private int customerId;
        private string customerName;

        public Customer(int customerId, string customerName)
        {
            this.customerId = customerId;
            this.customerName = customerName;
        }

        public int CustomerId
        {
            get => customerId;
            set => customerId = value;
        }

        public string CustomerName
        {
            get => customerName;
            set => customerName = value;
        }
    }
}
