using DataAccess;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class JobBUS
    {
        private EmployeeDAO _employeeDAO;
        private JobDAO _jobDAO;
        private ProjectDAO _projectDAO;

        public JobBUS()
        {
            _employeeDAO = new EmployeeDAO();
            _jobDAO = new JobDAO();
            _projectDAO = new ProjectDAO();
        }

        public string AddTask(dynamic task)
        {
            int idProject = _projectDAO.GetProjectIdByName(task.Project);
            if (idProject == 0)
                return "project not found";
            int idEmployee = _employeeDAO.GetEmployeeByUsernamePassword(task.Username, task.Password).Id;

            Job job = new Job
            {
                Name = task.Name,
                Description = task.Description,
                NumberOfHours = task.NumberOfHours,
                EmployeeId = idEmployee,
                ProjectId = idProject
            };
            _jobDAO.AddTask(job);
            return "ok";
        }
    }
}
