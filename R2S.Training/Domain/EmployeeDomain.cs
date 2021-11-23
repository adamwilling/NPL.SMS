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

        internal Employee SearchEmployeeById(int employeeId)
        {
            return employeeDao.SearchEmployeeById(employeeId);
        }

        internal bool AddEmployee(Employee employee)
        {
            return employeeDao.AddEmployee(employee);
        }

        internal bool DeleteEmployee(int employeeId)
        {
            if (employeeDao.SearchEmployeeById(employeeId) == null)
            {
                Console.WriteLine("***Mã nhân viên không tồn tại!!!");
                return false;
            }
            return employeeDao.DeleteEmployee(employeeId);
        }

        internal bool UpdateEmployee(Employee employee)
        {
            if (employeeDao.SearchEmployeeById(employee.EmployeeId) == null)
            {
                return false;
            }
            return employeeDao.UpdateEmployee(employee);
        }
    }
}
