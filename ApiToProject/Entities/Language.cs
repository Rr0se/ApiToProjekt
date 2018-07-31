using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Entities
{
    public class Language
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Podaj jezyk")]
        [MaxLength(20)]
        public string LanguageName { get; set; }

        [Required(ErrorMessage ="Uzupełnij pole.")]
        [Range(1,3,ErrorMessage = "Oceń w skali od 1 do 3")]
        public int SpeakingLevel { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole.")]
        [Range(1, 3, ErrorMessage = "Oceń w skali od 1 do 3")]
        public int WritingLevel { get; set; }

        [Required(ErrorMessage = "Uzupełnij pole.")]
        [Range(1, 3, ErrorMessage = "Oceń w skali od 1 do 3")]
        public int ReadingLevel { get; set; }

        public ICollection<EmployeeLanguage> EmployeeLanguages { get; set; }
    }
}
