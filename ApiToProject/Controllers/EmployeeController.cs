using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ApiToProject.Entities;
using ApiToProject.Services;
using ApiToProject.Models;
using AutoMapper;
using System;

namespace ApiToProject.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private IProfileGeneratorRepository _pgRepository;

        public EmployeeController(IProfileGeneratorRepository pgRepository)
        {
            _pgRepository = pgRepository;
        }
        
        [HttpGet()]
        public IActionResult GetEmployees()
        {
            var employeesFromRepo = _pgRepository.GetEmployees();
            var employees = Mapper.Map<IEnumerable<EmployeeDto>>(employeesFromRepo);
            return Ok(employees);
        }
        
        [HttpGet("{id}", Name = "GetEmployee")]
        public IActionResult GetEmployee(Guid id)
        {
            var employeeFromRepo = _pgRepository.GetEmployee(id);

            if (employeeFromRepo == null)
            {
                return NotFound();
            }

            var employee = Mapper.Map<EmployeeDto>(employeeFromRepo);
            return Ok(employee);
        }
        
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeForCreationDto employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            var employeeEntity = Mapper.Map<Employee>(employee);

            _pgRepository.AddEmployee(employeeEntity);

            if(!_pgRepository.Save())
            {
                throw new Exception("Creating an employee failed on save.");
            }

            var employeeToReturn = Mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("GetEmployee", new { id = employeeToReturn.Id }, employeeToReturn);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employeeFromRepo = _pgRepository.GetEmployee(id);
            if(employeeFromRepo == null)
            {
                return NotFound();
            }

            _pgRepository.DeleteEmployee(employeeFromRepo);

            if(!_pgRepository.Save())
            {
                throw new Exception($"Deleting author {id} failed on save.");
            }

            return NoContent();
        }

    }
}