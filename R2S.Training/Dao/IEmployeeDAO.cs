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
        Employee SearchEmployeeById(int employeeId);

    }
}
