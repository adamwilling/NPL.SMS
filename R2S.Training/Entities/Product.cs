using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2S.Training.Entities
{
    class Product
    {
        private int productId;
        private string productName;
        private double price;

        public Product(int productId, string productName, double price)
        {
            this.productId = productId;
            this.productName = productName;
            this.price = price;
        }

        public int ProductId
        {
            get => productId;
        }

        public string ProductName
        {
            get => productName;
        }

        public double Price
        {
            get => price;
        }
    }
}
