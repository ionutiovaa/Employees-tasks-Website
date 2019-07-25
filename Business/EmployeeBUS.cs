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

        public dynamic GetEmployeeById(int id)
        {
            Employee employeeDAO = _employeeDAO.GetEmployeeById(id);
            dynamic employee = new ExpandoObject();
            employee.Id = id;
            employee.LastName = employeeDAO.LastName;
            employee.FirstName = employeeDAO.FirstName;
            employee.UserType = employeeDAO.UserType;
            return employee;
        }

        public dynamic GetEmployeeByUsername(string username)
        {
            Employee employeeDAO = _employeeDAO.GetEmployeeByUsername(username);
            dynamic employee = new ExpandoObject();
            employee.FirstName = employeeDAO.FirstName;
            employee.LastName = employeeDAO.LastName;
            return employee;
        }

        public dynamic GetEmployeeByUsernamePassword(string username, string password)
        {
            Employee employeeDAO = _employeeDAO.GetEmployeeByUsernamePassword(username, password);
            if (employeeDAO == null)
                return null;
            else
            {
                dynamic employee = new ExpandoObject();
                employee.Id = employeeDAO.Id;
                employee.LastName = employeeDAO.LastName;
                employee.FirstName = employeeDAO.FirstName;
                employee.Username = employeeDAO.Username;
                employee.Password = employeeDAO.Password;
                employee.UserType = employeeDAO.UserType;
                return employee;
            }
        }

        public string GetPasswordByUser(string username)
        {
            return _employeeDAO.GetPasswordByUser(username);
        }

        public void Change(string username, string newPassword)
        {
            _employeeDAO.Change(username, newPassword);
        }

        public List<dynamic> GetEmployeeByName(string lastName, string firstName)
        {
            return _employeeDAO.GetEmployeeByName(lastName, firstName);
        }

        public void AddEmployee(dynamic employee)
        {
            _employeeDAO.AddEmployee(employee);
        }

        public void DeleteEmployee(string lastName, string firstName)
        {
            _employeeDAO.DeleteEmployee(lastName, firstName);
        }

        public void EditEmployee(int id, string lastName, string firstName, string username, string password, int userType)
        {
            _employeeDAO.EditEmployee(id, lastName, firstName, username, password, userType);
        }

        public int GetIdEmployee(string username, string password)
        {
            return _employeeDAO.GetIdEmployee(username, password);
        }

        public List<dynamic> GetEmployees()
        {
            List<Employee> employeesDAO = _employeeDAO.GetEmployees();
            List<dynamic> employees = new List<dynamic>();

            foreach(var employee in employeesDAO)
            {
                dynamic e = new ExpandoObject();
                e.Id = employee.Id;
                e.LastName = employee.LastName;
                e.FirstName = employee.FirstName;
                e.Username = employee.Username;
                e.Password = employee.Password;
                e.UserType = employee.UserType;
                employees.Add(e);
            }

            return employees;
        }
    }
}
