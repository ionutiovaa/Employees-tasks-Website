using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskApp.Models
{
    public class EmployeeModelForView
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Description { get; set; }

        public float NumberOfHours { get; set; }

        public string ProjectName { get; set; }
    }
}