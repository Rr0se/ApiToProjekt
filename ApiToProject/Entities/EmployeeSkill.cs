using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Entities
{
    public class EmployeeSkill
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }
        public Guid SkillId { get; set; }

        public Skill Skill { get; set; }
        public Employee Employee { get; set; }
    }
}
