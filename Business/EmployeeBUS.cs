using DataAccess;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class EmployeeBUS
    {
        private EmployeeDAO _employeeDAO;

        public EmployeeBUS()
        {
            _employeeDAO = new EmployeeDAO();
        }

        //public Employee GetEmployeeById(int id)
        //{
        //    return _employeeDAO.GetEmployeeById(id);
        //}

        public dynamic GetEmployeeById(int id)
        {
            Employee employeeDAO = _employeeDAO.GetEmployeeById(id);
            dynamic employee = new ExpandoObject();
            employee.Id = id;
            employee.LastName = employeeDAO.LastName;
            employee.FirstName = employeeDAO.FirstName;
            employee.TaskId = employeeDAO.TaskId;
            employee.UserType = employeeDAO.UserType;
            return employee;
        }
    }
}
