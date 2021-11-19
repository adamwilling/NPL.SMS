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
    class ProductDAO : IProductDAO
    {
        ConnectionManagers db = null;
        public ProductDAO()
        {
            db = new ConnectionManagers();
        }
        public bool AddProduct(Product product)
        {
            return db.InsertProduct(product);
        }

        public List<Product> SearchAllProduct()
        {
            try
            {
                List<Product> listProduct = null;
                DataTable dt = db.SearchProduct();
                if (dt.Rows.Count > 0)
                {
                    listProduct = new List<Product>();
                    foreach (DataRow row in dt.Rows)
                    {
                        int productId = int.Parse(row["product_id"].ToString());
                        string productName = row["product_name"].ToString();
                        double price = double.Parse(row["product_price"].ToString());
                        Product product = new Product(productId, productName, price);
                        listProduct.Add(product);
                    }
                }
                return listProduct;
            }
            catch
            {
                return null;
            }
        }

        public Product SearchProductById(int product_id)
        {
            try
            {
                Product product = null;
                DataTable dt = db.SearchProductById(product_id);
                if (dt.Rows.Count > 0)
                {
                    int productId = int.Parse(dt.Rows[0]["product_id"].ToString());
                    string productName = dt.Rows[0]["product_name"].ToString();
                    double price = double.Parse(dt.Rows[0]["product_price"].ToString());
                    product = new Product(productId, productName, price);
                }
                return product;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateProduct(Product product)
        {
            return db.UpdateProduct(product);
        }
    }
}
