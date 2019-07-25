using Business;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskApp.Models;
using OfficeOpenXml;
using System.IO;
using System.Data;
using OfficeOpenXml.Table;

namespace TaskApp.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployee()
        {
            string username = Session["User"].ToString();
            EmployeeBUS service = new EmployeeBUS();
            string password = service.GetPasswordByUser(username);
            var userFromDAO = service.GetEmployeeByUsernamePassword(username, password);
            var employee = new Employee
            {
                FirstName = userFromDAO.FirstName,
                LastName = userFromDAO.LastName
            };
            return RedirectToAction("GetEmployeeByName", new { lastName = employee.LastName, firstName = employee.FirstName});
        }

        public ActionResult NewTask()
        {
            return View();
        }

        [EmployeeAuthorization]
        public ActionResult AddTask(Job task)
        {
            EmployeeBUS employeeBUS = new EmployeeBUS();
            JobBUS service = new JobBUS();
            TempData["returned"] = null;
            dynamic t = new ExpandoObject();
            t.Name = task.Name;
            t.Description = task.Description;
            t.NumberOfHours = task.NumberOfHours;
            t.Project = task.Project;
            t.Username = Session["User"];
            t.Password = employeeBUS.GetPasswordByUser(Session["User"].ToString());
            string returned = service.AddTask(t);
            if (returned.Equals("project not found"))
            {
                TempData["returned"] = "not";
                return View("NewTask");
            }
            else
                return RedirectToAction("GetEmployee");
        }

        public ActionResult NewEmployee()
        {
            return View();
        }

        [UserAuthorization]
        public ActionResult AddEmployee(Employee employee)
        {
            dynamic e = new ExpandoObject();
            e.FirstName = employee.FirstName;
            e.LastName = employee.LastName;
            e.Username = employee.Username;
            e.Password = employee.Password;
            if (employee.UserType.Equals("Admin"))
                e.UserType = 1;
            else e.UserType = 2;
            EmployeeBUS service = new EmployeeBUS();
            service.AddEmployee(e);
            return RedirectToAction("GetEmployees");
        }

        [UserAuthorization]
        public ActionResult DeleteEmployee(string lastName, string firstName)
        {
            EmployeeBUS service = new EmployeeBUS();
            service.DeleteEmployee(lastName, firstName);
            return RedirectToAction("GetEmployees");
        }

        public ActionResult GetIdEmployee(string lastName, string firstName, string username, string password, string userType)
        {
            string l = lastName;
            string f = firstName;
            string u = userType;
            EmployeeBUS service = new EmployeeBUS();
            int id = service.GetIdEmployee(username, password);
            Employee employee = new Employee { Id = id, LastName = lastName, FirstName = firstName, Username = username, Password = password, UserType = userType };
            return View("EditEmployee", employee);
        }

        [UserAuthorization]
        public ActionResult EditEmployee(int id, string lastName, string firstName, string username, string password, string userType)
        {
            EmployeeBUS service = new EmployeeBUS();
            int userT;
            if (userType.Equals("Admin"))
                userT = 1;
            else userT = 2;
            service.EditEmployee(id, lastName, firstName, username, password, userT);
            var employeesBUS = service.GetEmployees();
            List<Employee> employees = new List<Employee>();
            foreach (var employee in employeesBUS)
            {
                Employee e = new Employee { FirstName = employee.FirstName, LastName = employee.LastName };
                employees.Add(e);
            }
            return RedirectToAction("GetEmployees");
        }

        public ActionResult GetEmployeeByName(string lastName, string firstName)
        {
            EmployeeBUS service = new EmployeeBUS();
            var employeeBUS = service.GetEmployeeByName(lastName, firstName);
            EmployeesInView inView = new EmployeesInView { EmployeesForView = new List<EmployeeModelForView>() };
            if (employeeBUS.Count == 0)
            {
                EmployeeModelForView forView = new EmployeeModelForView
                {
                    LastName = lastName,
                    FirstName = firstName
                };
                inView.EmployeesForView.Add(forView);
            }
            else
            {
                foreach (var e in employeeBUS)
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
            }


            //if (int.Parse(Session["UserType"].ToString()) == 1)
            //    return View(inView);
            //else
                return View("YourPage", inView);
        }

        public ActionResult ExcelExport(string fName, string lName)
        {
            EmployeeBUS service = new EmployeeBUS();
            string firstName = fName;
            string lastName = lName;
            firstName = "John";
            lastName = "Smith";
            //string lastName = "";
            //string firstName = "";
            //if (int.Parse(Session["UserType"].ToString()) != 1)
            //{
            //    var employee = service.GetEmployeeByUsername(Session["User"].ToString());
            //    lastName = employee.LastName;
            //    firstName = employee.FirstName;
            //}
            //else
            //{
            //    firstName = fName;
            //    lastName = lName;
            //}

            var employeeBUS = service.GetEmployeeByName(lastName, firstName);
            EmployeesInView inView = new EmployeesInView { EmployeesForView = new List<EmployeeModelForView>() };
            if (employeeBUS.Count == 0)
            {
                EmployeeModelForView forView = new EmployeeModelForView
                {
                    LastName = lastName,
                    FirstName = firstName
                };
                inView.EmployeesForView.Add(forView);
            }
            else
            {
                foreach (var e in employeeBUS)
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
            }
            try
            {
                DataTable dataTable = new DataTable();
                if (int.Parse(Session["UserType"].ToString()) != 1)
                {
                    //dataTable.Columns.Add("First name", typeof(string));
                    //dataTable.Columns.Add("Last name", typeof(string));
                    dataTable.Columns.Add("Task", typeof(string));
                    dataTable.Columns.Add("Description", typeof(string));
                    dataTable.Columns.Add("Number of hours", typeof(float));
                    dataTable.Columns.Add("Project", typeof(string));

                    foreach (var e in inView.EmployeesForView)
                    {
                        DataRow row = dataTable.NewRow();
                        //row[0] = e.FirstName;
                        //row[1] = e.LastName;
                        row[0] = e.TaskName;
                        row[1] = e.Description;
                        row[2] = e.NumberOfHours;
                        row[3] = e.ProjectName;
                        dataTable.Rows.Add(row);
                    }
                }
                else
                {
                    dataTable.Columns.Add("First name", typeof(string));
                    dataTable.Columns.Add("Last name", typeof(string));
                    dataTable.Columns.Add("Task", typeof(string));
                    dataTable.Columns.Add("Description", typeof(string));
                    dataTable.Columns.Add("Number of hours", typeof(float));
                    dataTable.Columns.Add("Project", typeof(string));

                    foreach (var e in inView.EmployeesForView)
                    {
                        DataRow row = dataTable.NewRow();
                        row[0] = e.FirstName;
                        row[1] = e.LastName;
                        row[0] = e.TaskName;
                        row[1] = e.Description;
                        row[2] = e.NumberOfHours;
                        row[3] = e.ProjectName;
                        dataTable.Rows.Add(row);
                    }
                }

                var memoryStream = new MemoryStream();
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    worksheet.Cells["A1"].LoadFromDataTable(dataTable, true, TableStyles.None);
                    worksheet.Cells["A1:AN1"].Style.Font.Bold = true;
                    worksheet.DefaultRowHeight = 18;

                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.DefaultColWidth = 20;
                    worksheet.Column(2).AutoFit();

                    Session["DownloadExcel_FileManager"] = excelPackage.GetAsByteArray();
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public ActionResult Download()
        {

            if (Session["DownloadExcel_FileManager"] != null)
            {
                byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                return File(data, "application/octet-stream", "FileManager.xlsx");
            }
            else
            {
                return new EmptyResult();
            }
        }

        [UserAuthorization]
        public ActionResult GetEmployees()
        {
            EmployeeBUS service = new EmployeeBUS();
            var employeesBUS = service.GetEmployees();
            List<Employee> employees = new List<Employee>();
            foreach (var employee in employeesBUS)
            {
                Employee e = new Employee { FirstName = employee.FirstName, LastName = employee.LastName, Username = employee.Username, Password = employee.Password };
                employees.Add(e);
            }
            return View(employees);
        }

        public ActionResult ChangePassword()
        {
            Employee employee = new Employee
            {
                Username = Session["User"].ToString()
            };
            return View(employee);
        }

        public ActionResult Change(string username, string password)
        {
            TempData["pass"] = null;
            string newPassword = password;
            //string pass = Session["Password"].ToString();
            EmployeeBUS service = new EmployeeBUS();
            string pass = service.GetPasswordByUser(username);
            //var userFromDAO = service.GetEmployeeByUsernamePassword(username, pass);
            if (pass.Equals(newPassword))
            {
                TempData["pass"] = "same";
                return View("ChangePassword");
            }
            else
            {
                service.Change(username, newPassword);
                //Session["Password"] = newPassword;
                if (int.Parse(Session["UserType"].ToString()) == 1)
                    return RedirectToAction("GetEmployees");
                else return RedirectToAction("GetEmployee");
            }
        }
    }
}