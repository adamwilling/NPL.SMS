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
    class EmpoloyeeDAO : IEmployeeDAO
    {
        ConnectionManagers db = null;
        public EmpoloyeeDAO()
        {
            db = new ConnectionManagers();
        }
        public bool AddEmployee(Employee employee)
        {
            return db.AddEmployyee(employee);
        }

        public List<Employee> GetAllEmployee()
        {
            try
            {
                List<Employee> listEmployee = null;
                DataTable dt = db.SearchAllEmployee();
                if (dt.Rows.Count > 0)
                {
                    listEmployee = new List<Employee>();
                    foreach (DataRow row in dt.Rows)
                    {
                        int employeeId = int.Parse(row["employee_id"].ToString());
                        string name = row["employee_name"].ToString();
                        double salary = double.Parse(row["salary"].ToString());
                        int spvr = int.Parse(row["supervisor_id"].ToString());
                        Employee employee = new Employee(employeeId, name, salary, spvr);
                        listEmployee.Add(employee);
                    }
                }
                return listEmployee;
            }
            catch
            {
                return null;
            }
        }

        public Employee GetEmployeeById(int employee_id)
        {
            try
            {
                Employee employee = null;
                DataTable dt = db.SearchemployeeByid(employee_id);
                if (dt.Rows.Count > 0)
                {
                        int employeeId = int.Parse(dt.Rows[0]["employee_id"].ToString());
                        string name = dt.Rows[0]["employee_name"].ToString();
                        double salary = double.Parse(dt.Rows[0]["salary"].ToString());
                        int spvr = int.Parse(dt.Rows[0]["supervisor_id"].ToString());
                        employee = new Employee(employeeId, name, salary, spvr);
                }
                return employee;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateEmployee(Employee employee)
        {
            return db.UpdateEmployeeById(employee);
        }
    }
}
