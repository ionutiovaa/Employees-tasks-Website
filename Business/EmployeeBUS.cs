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

        public List<dynamic> GetEmployees()
        {
            List<Employee> employeesDAO = _employeeDAO.GetEmployees();
            List<dynamic> employees = new List<dynamic>();
            //for (int i = 0; i < employeesDAO.Count; i++)
            //{
            //    employees[i].Id = employeesDAO[i].Id;
            //    employees[i].LastName = employeesDAO[i].LastName;
            //    employees[i].FirstName = employeesDAO[i].FirstName;
            //    employees[i].TaskId = employeesDAO[i].TaskId;
            //    employees[i].UserType = employeesDAO[i].UserType;
            //}

            foreach(var employee in employeesDAO)
            {
                dynamic e = new ExpandoObject();
                e.Id = employee.Id;
                e.LastName = employee.LastName;
                e.FirstName = employee.FirstName;
                e.TaskId = employee.TaskId;
                e.UserType = employee.UserType;
                employees.Add(e);
            }

            return employees;
        }
    }
}
