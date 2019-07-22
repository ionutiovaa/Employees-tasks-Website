using Business;
using System;
using System.Collections.Generic;
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

        public ActionResult GetEmployeeByName(string lastName, string firstName)
        {
            EmployeeBUS service = new EmployeeBUS();
            var employeeBUS = service.GetEmployeeByName(lastName, firstName);
            EmployeeModelForView forView = new EmployeeModelForView
            {
                LastName = lastName,
                FirstName = firstName,
                NumberOfHours = employeeBUS.NumberOfHours,
                Description = employeeBUS.Description,
                ProjectName = employeeBUS.ProjectName
            };
            return View(forView);
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