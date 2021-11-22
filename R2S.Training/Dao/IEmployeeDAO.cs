using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using R2S.Training.Entities;
using System.Data;
namespace R2S.Training.Dao
{
    interface IEmployeeDAO
    {
        List<Employee> GetAllEmployee();

        Employee SearchEmployeeById(int employeeId);

        bool AddEmployee(Employee employee);

        bool DeleteEmployee(int employeeId);

        bool UpdateEmployee(Employee employee);

    }
}
