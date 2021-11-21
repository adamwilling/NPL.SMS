using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2S.Training.Entities
{
    class LineItem
    {
        private int orderId;
        private int productId;
        private int quantity;
        private double price;

        public LineItem(int orderId, int productId, int quantity, double price)
        {
            this.orderId = orderId;
            this.productId = productId;
            this.quantity = quantity;
            this.price = price;
        }

        #region Property
        public int OrderId
        {
            get => orderId;
        }

        public int ProductId
        {
            get => productId;
        }

        public int Quantity
        {
            get => quantity;
        }

        public double Price
        {
            get => price;
            set => price = value;
        }
        #endregion
    }
}
