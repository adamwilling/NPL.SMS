using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2S.Training.Entities
{
    class Employee
    {
        private int employeeId;
        private string employeeName;
        private double salary;
        private int spvrId;

        public Employee()
        {

        }
        public Employee(int employeeId, string employeeName, double salary, int spvrId)
        {
            this.employeeId = employeeId;
            this.employeeName = employeeName;
            this.salary = salary;
            this.spvrId = spvrId;
        }

        #region Property
        public int EmployeeId
        {
            get => employeeId;
        }

        public string EmployeeName
        {
            get => employeeName;
        }

        public double Salary
        {
            get => salary;
        }

        public int SpvrId
        {
            get => spvrId;
        }
        #endregion
    }
}
