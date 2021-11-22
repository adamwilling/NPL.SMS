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
        internal List<Employee> GetAllEmployee()
        {
            return employeeDao.GetAllEmployee();
        }

        internal Employee SearchEmployeeById(int EmployeeId)
        {
            return employeeDao.SearchEmployeeById(EmployeeId);
        }

        internal bool AddEmployee(Employee Employee)
        {
            return employeeDao.AddEmployee(Employee);
        }

        internal bool DeleteEmployee(int EmployeeId)
        {
            return employeeDao.DeleteEmployee(EmployeeId);
        }

        internal bool UpdateEmployee(Employee Employee)
        {
            if (employeeDao.SearchEmployeeById(Employee.EmployeeId) == null)
            {
                return false;
            }
            return employeeDao.UpdateEmployee(Employee);
        }
    }
}
