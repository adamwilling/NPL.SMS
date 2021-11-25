using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.ConnectionManager;
using R2S.Training.Entities;

namespace R2S.Training.Dao
{
    class LineItemDAO : ILineItemDAO
    {
        ConnectionManagers db = null;
        public LineItemDAO()
        {
            db = new ConnectionManagers();
        }
        public bool AddLineItem(LineItem lineItem)
        {
                return db.AddLineItem(lineItem);
        }

        public List<LineItem> GetAllItemsByOrderId(int orderId)
        {
            return db.GetAllItemsByOrderId(orderId);
        }
    }
}
