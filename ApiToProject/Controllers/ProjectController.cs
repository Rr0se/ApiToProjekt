using ApiToProject.Entities;
using ApiToProject.Models;
using ApiToProject.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiToProject.Controllers
{
    [Route("api/employees/{employeeId}/projects")]
    public class ProjectController : Controller
    {
        private IProfileGeneratorRepository _pgRepository;

        public ProjectController(IProfileGeneratorRepository pgRepository)
        {
            _pgRepository = pgRepository;
        }

        /*--------------------------------------------------
         *   -----------------------------------------------*/

        [HttpGet()]
        public IActionResult GetProjectsForEmployee(Guid employeeId)
        {

            if (!_pgRepository.GetProjectForEmployee(employeeId))
            {
                return NotFound();
            }

            var projectForEmployeeFromRepo = _pgRepository.GetProjectForEmployee(employeeId);
            var projectForEmployee = Mapper.Map<IEnumerable<ProjectsDto>>(projectForEmployeeFromRepo);

            return Ok(projectForEmployee);

            // return new JsonResult(employeeFromRepo);
        }

        /*--------------------------------------------------
        *   -----------------------------------------------*/

        [HttpGet("{id}", Name = "GetProjectForEmployee")]
        public IActionResult GetProjectForEmployee(Guid employeeId,Guid Id)
        {
            if (!_pgRepository.GetProjectForEmployee(employeeId))
            {
                return NotFound();
            }
            var projectForEmployeeFromRepo = _pgRepository.GetBookForAuthor(employeeId, id);
            if (projectForEmployeeFromRepo == null)
            {
                return NotFound();
            }

            var projectForEmployee = Mapper.Map<ProjectsDto>(projectForEmployeeFromRepo);
            return Ok(projectForEmployee);
        }

        /*--------------------------------------------------
        *   -----------------------------------------------*/

        [HttpPost()]
        public IActionResult CreateProjectForEmployee(Guid employeeId, [FromBody] ProjectsForCreationDto project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            
            if (!_pgRepository.GetEmployee(employeeId))
            {
                return NotFound();
            }

            var projectEntity = Mapper.Map<Project>(project);

            _pgRepository.AddProjectForEmployee(employeeId, projectEntity);

            if (!_pgRepository.Save())
            {
                throw new Exception($"Deleting tittle {employeeId} failed on save.");
            }
            var projectToReturn = Mapper.Map<ProjectsDto>(projectEntity);

            return CreatedAtRoute("GetProjectForEmployee",
                new { employeeId = employeeId, id = projectToReturn.Id },
                projectToReturn);


        }

        /*--------------------------------------------------
        *   -----------------------------------------------*/

        [HttpDelete("{id}")]
        public IActionResult DeleteProject(Guid employeeId,Guid id)
        {
           
            if (!_pgRepository.GetEmployee(employeeId))
            {
                return NotFound();
            }
            var projectForEmployeeFromRepo = _pgRepository.GetProjectForEmployee(employeeId, id);
            if (projectForEmployeeFromRepo == null)
            {
                return NotFound();
            }

            _pgRepository.DeleteProject(projectForEmployeeFromRepo);

            if (!_pgRepository.Save())
            {
                throw new Exception($"Deleting tittle {id} failed on save.");
            }

            return NoContent();
        }
    }
}
