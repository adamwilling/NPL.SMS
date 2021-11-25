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
        private double productPrice;

        public Product(int productId, string productName, double productPrice)
        {
            this.productId = productId;
            this.productName = productName;
            this.productPrice = productPrice;
        }

        public int ProductId
        {
            get => productId;
        }

        public string ProductName
        {
            get => productName;
        }

        public double ProductPrice
        {
            get => productPrice;
        }
    }
}
