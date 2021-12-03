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
    class EmployeeDAO : IEmployeeDAO
    {
        ConnectionManagers db = null;
        public EmployeeDAO()
        {
            db = new ConnectionManagers();
        }

        public Employee SearchEmployeeById(int employeeId)
        {
            return db.SearchEmployeeById(employeeId);
        }
    }
}
