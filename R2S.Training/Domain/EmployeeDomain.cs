using R2S.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.Dao;
namespace R2S.Training.Domain
{
    class EmployeeDomain
    {
        EmployeeDAO employeeDao = null;
        public EmployeeDomain()
        {
            employeeDao = new EmployeeDAO();
        }

        internal Employee SearchEmployeeById(int employeeId)
        {
            return employeeDao.SearchEmployeeById(employeeId);
        }
    }
}
