using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToProject.Models
{
    public class LanguagesDto
    {
        public Guid Id { get; set; }
        public string LanguageName { get; set; }
        public int SpeakingLevel { get; set; }
        public int WritingLevel { get; set; }
        public int ReadingLevel { get; set; }
    }
}
