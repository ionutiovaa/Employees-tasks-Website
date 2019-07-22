using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Job
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float NumberOfHours { get; set; }

        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }
    }
}
