using Business;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskApp.Models;

namespace TaskApp.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployee(/*int id*/)
        {
            EmployeeBUS service = new EmployeeBUS();
            var employeeBUS = service.GetEmployeeById(1);
            string lastName = employeeBUS.LastName;
            string firstName = employeeBUS.FirstName;
            Employee employee = new Employee { LastName = lastName, FirstName = firstName };
            return View(employee);
        }

        public ActionResult NewEmployee()
        {
            return View();
        }

        public ActionResult AddEmployee(Employee employee)
        {
            dynamic e = new ExpandoObject();
            e.FirstName = employee.FirstName;
            e.LastName = employee.LastName;
            if (employee.UserType.Equals("Admin"))
                e.UserType = 1;
            else e.UserType = 2;
            EmployeeBUS service = new EmployeeBUS();
            service.AddEmployee(e);
            var employeesBUS = service.GetEmployees();
            Employees employees = new Employees { EmployeesList = new List<Employee>() };
            foreach (var emp in employeesBUS)
            {
                Employee em = new Employee { FirstName = emp.FirstName, LastName = emp.LastName };
                employees.EmployeesList.Add(em);
            }
            return View("GetEmployees", employees);
        }

        public ActionResult DeleteEmployee(string lastName, string firstName)
        {
            EmployeeBUS service = new EmployeeBUS();
            service.DeleteEmployee(lastName, firstName);
            var employeesBUS = service.GetEmployees();
            Employees employees = new Employees { EmployeesList = new List<Employee>() };
            foreach (var emp in employeesBUS)
            {
                Employee em = new Employee { FirstName = emp.FirstName, LastName = emp.LastName };
                employees.EmployeesList.Add(em);
            }
            return View("GetEmployees", employees);
        }

        public ActionResult GetIdEmployee(string lastName, string firstName, string userType)
        {
            EmployeeBUS service = new EmployeeBUS();
            int id = service.GetIdEmployee(lastName, firstName);
            Employee employee = new Employee { Id = id, LastName = lastName, FirstName = firstName, UserType = userType };
            return View("EditEmployee", employee);
        }

        public ActionResult EditEmployee(int id, string lastName, string firstName, string userType)
        {
            EmployeeBUS service = new EmployeeBUS();
            int userT;
            if (userType.Equals("Admin"))
                userT = 1;
            else userT = 2;
            service.EditEmployee(id, lastName, firstName, userT);
            var employeesBUS = service.GetEmployees();
            Employees employees = new Employees { EmployeesList = new List<Employee>() };
            foreach (var emp in employeesBUS)
            {
                Employee em = new Employee { FirstName = emp.FirstName, LastName = emp.LastName };
                employees.EmployeesList.Add(em);
            }
            return View("GetEmployees", employees);
        }

        public ActionResult GetEmployeeByName(string lastName, string firstName)
        {
            EmployeeBUS service = new EmployeeBUS();
            var employeeBUS = service.GetEmployeeByName(lastName, firstName);
            EmployeesInView inView = new EmployeesInView { EmployeesForView = new List<EmployeeModelForView>() };
            foreach(var e in employeeBUS)
            {
                EmployeeModelForView forView = new EmployeeModelForView
                {
                    LastName = lastName,
                    FirstName = firstName,
                    NumberOfHours = e.NumberOfHours,
                    TaskName = e.TaskName,
                    Description = e.Description,
                    ProjectName = e.ProjectName
                };
                inView.EmployeesForView.Add(forView);
            }
            
            return View(inView);
        }

        public ActionResult GetEmployees()
        {
            EmployeeBUS service = new EmployeeBUS();
            var employeesBUS = service.GetEmployees();
            Employees employees = new Employees { EmployeesList = new List<Employee>() };
            foreach (var employee in employeesBUS)
            {
                Employee e = new Employee { FirstName = employee.FirstName, LastName = employee.LastName };
                employees.EmployeesList.Add(e);
            }
            return View(employees);
        }
    }
}