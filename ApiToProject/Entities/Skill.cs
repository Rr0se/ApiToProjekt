using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Entities
{
    public class Skill
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Uzupełnij pole")]
        [MaxLength(20)]
        public string SkillName { get; set; }

        [Required(ErrorMessage ="Uzupełnij pole")]
        public double ExperienceInYears { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole")]
        [Range(1,5,ErrorMessage ="Oceń w skali od 1 do 5")]
        public int Profficiency { get; set; }

        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
