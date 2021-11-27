using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.Dao;
using R2S.Training.Entities;
namespace R2S.Training.Domain
{
    class LineItemDomain
    {
        LineItemDAO lineItemDAO = null;
        OrderDAO orderDAO = null;
        ProductDAO productDAO = null;
        public LineItemDomain()
        {
            lineItemDAO = new LineItemDAO();
            orderDAO = new OrderDAO();
            productDAO = new ProductDAO();
        }
        
        public List<LineItem> GetAllItemsByOrderId(int orderId)
        {
            if (orderDAO.SearchOrderById(orderId) == null)
            {
                Console.WriteLine("*** Order id not found!!!");
                return null;
            }
            return lineItemDAO.GetAllItemsByOrderId(orderId);
        }

        public bool AddLineItem(LineItem lineItem)
        {
            Order order = orderDAO.SearchOrderById(lineItem.OrderId);
            Product product = productDAO.SearchProductById(lineItem.ProductId);
            if (order == null)
            {
                Console.WriteLine("*** Order id not found!!!");
                if (product == null)
                {
                    Console.WriteLine("*** Product id not found!!!");
                    return false;
                }
                return false;
            }
            if (product == null)
            {
                Console.WriteLine("**** Product id not found!!!");
                if (order == null)
                {
                    Console.WriteLine("*** Order id not found!!!");
                    return false;
                }
                return false;
            }

            return lineItemDAO.AddLineItem(lineItem);
        }
    }
}
