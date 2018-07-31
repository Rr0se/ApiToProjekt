using ApiToProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiToProject.Services
{
    public class ProfileGeneratorRepository : IProfileGeneratorRepository
    {
        private DataBaseContext _context;

        public ProfileGeneratorRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _context.Employees.Add(employee);

            if (employee.Projects.Any())
            {
                foreach (var project in employee.Projects)
                {
                    project.Id = Guid.NewGuid();
                }
            }
        }

        public void AddProjectforEmployee(Guid employeeId, Project project)
        {
            var employee = GetEmployee(employeeId);
            if (employee != null)
            {
                if (employee.Id == Guid.Empty)
                {
                    employee.Id = Guid.NewGuid();
                }
                employee.Projects.Add(project);
            }
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public void DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
        }

        public Employee GetEmployee(Guid employeeId)
        {
            return _context.Employees.FirstOrDefault(a => a.Id == employeeId);
        }
        
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();
        }

        public IEnumerable<Employee> GetEmployees(IEnumerable<Guid> employeeIds)
        {
            return _context.Employees.Where(a => employeeIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }

        public void UpdateEmployee(Employee employee)
        {
            // no code in this implementation
        }

        public Project GetProjectForEmployee(Guid employeeId, int projectId)
        {
            return _context.Projects
                .Where(b => b.EmployeeId == employeeId && b.Id == projectId).FirstOrDefault();
        }
        /*--------------------------------------------------
       *   -----------------------------------------------*/
        public IEnumerable<Project> GetProject(Guid employeeId)
        {
            return _context.Projects
                        .Where(b => b.EmployeeId == employeeId).OrderBy(b => b.Title).ToList();
        }

        public void UpdateProject(Project project)
        {
            // no code in this implementation
        }


        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
