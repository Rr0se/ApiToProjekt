using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Models
{
    public class EmployeeForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public int Rating { get; set; }
        public int YearsOfWork { get; set; }

        public ICollection<ProjectsForCreationDto> Projects { get; set; }
        = new List<ProjectsForCreationDto>();

        public ICollection<SkillsForCreationDto> Skills { get; set; }
        = new List<SkillsForCreationDto>();

        public ICollection<LanguagesForCreationDto> Languages { get; set; }
        = new List<LanguagesForCreationDto>();
    }
}
