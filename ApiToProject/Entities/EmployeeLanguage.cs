using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Entities
{
    public class EmployeeLanguage
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }
        public Guid LanguageId { get; set; }

        public Employee Employee { get; set; }
        public Language Language { get; set; }
    }
}
