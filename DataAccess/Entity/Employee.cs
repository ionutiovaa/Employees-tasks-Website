using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Employee
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int TaskId { get; set; }

        public int UserType { get; set; }
    }
}
