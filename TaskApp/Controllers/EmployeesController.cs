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
    }
}