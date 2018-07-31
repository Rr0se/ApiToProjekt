using ApiToProject.Entities;
using ApiToProject.Helpers;
using ApiToProject.Models;
using ApiToProject.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Controllers
{
    [Route("api/employeecollections")]
    public class EmployeeCollectionsController : Controller
    {
        private IProfileGeneratorRepository _pgRepository;

        public EmployeeColletionsController(IProfileGeneratorRepository pgRepository)
        {
            _pgRepository = pgRepository;
        }

        [HttpPost]
        public IActionResult CreateEmployeeColletion([FromBody]IEnumerable<EmployeeForCreationDto> employeeCollection)
        {
            if (employeeCollection == null)
            {
                return BadRequest();
            }

            var employeeEntities = Mapper.Map<IEnumerable<Employee>>(employeeCollection);

            foreach (var employee in employeeEntities)
            {
                _pgRepository.AddEmployee(employee);
            }

            if (!_pgRepository.Save())
            {
                throw new Exception("Creating an employee failed on save.");
            }

            var employeeCollectionToReturn = Mapper.Map<EmployeeDto>(employeeEntities);
            var idsAsString = string.Join(",",
                employeeCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetEmployeeCollection",
                new { ids = idsAsString },
                employeeCollectionToReturn);
            //return Ok();
        }

        [HttpGet("({ids})", Name = "GetEmployeeCollection")]
        public IActionResult GetEmployeeCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var employeeEntities = _pgRepository.GetEmployees(ids);

            if (ids.Count() != employeeEntities.Count())
            {
                return NotFound();
            }

            var employeesToReturn = Mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
            return Ok(employeesToReturn);
        }
    }
}
