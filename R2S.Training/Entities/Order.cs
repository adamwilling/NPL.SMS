using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2S.Training.Entities
{
    class Order
    {
        private int orderId;
        private DateTime orderDate;
        private int customerId;
        private int employeeId;
        private double total;

        public Order(int orderId, DateTime orderDate, int customerId, int employeeId, double total)
        {
            this.orderId = orderId;
            this.orderDate = orderDate;
            this.customerId = customerId;
            this.employeeId = employeeId;
            this.total = total;
        }

        #region Property
        public int OrderId
        {
            get => orderId;
        }

        public DateTime OrderDate
        {
            get => orderDate;
        }

        public int CustomerId
        {
            get => customerId;
        }

        public int EmployeeId
        {
            get => employeeId;
        }

        public double Total
        {
            get => total;
        }
        #endregion
    }
}
