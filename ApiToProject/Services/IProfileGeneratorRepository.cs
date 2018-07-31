using System;
using System.Collections.Generic;
using ApiToProject.Entities;

namespace ApiToProject.Services
{
    public interface IProfileGeneratorRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(Guid employeeId);
        IEnumerable<Employee> GetEmployees(IEnumerable<Guid> employeeIds);
        void AddEmployee(Employee employee);
        void EditEmployee(Guid id);
        void EditEmployee(Guid id, Employee e);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        
        IEnumerable<Project> GetProjectsForEmployee(IEnumerable<Guid> projectId);
        Project GetProjectForEmployee(Guid employeeId, Guid projectId);
        void AddProjectForEmployee(Guid employeeId, Project project);
        void EditProject(Guid id);
        void EditProject(Guid id, Employee e);
        void UpdateProjectForEmployee(Project project);
        void DeleteProject(Project project);
        bool Save();
    }
}
