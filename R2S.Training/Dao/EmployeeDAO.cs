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
        public bool AddEmployee(Employee employee)
        {
            return db.AddEmployee(employee);
        }

        public List<Employee> GetAllEmployee()
        {
            return db.GetAllEmployee();
        }

        public Employee SearchEmployeeById(int employee_id)
        {
            return db.SearchEmployeeById(employee_id);
        }

        public bool UpdateEmployee(Employee employee)
        {
            return db.UpdateEmployeeById(employee);
        }
    }
}
