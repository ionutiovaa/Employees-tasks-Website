using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskApp.Models
{
    public class EmployeesInView
    {
        public List<EmployeeModelForView> EmployeesForView { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}