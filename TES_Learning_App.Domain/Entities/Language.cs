using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TES_Learning_App.Domain.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string LanguageName { get; set; } = string.Empty;

        // Navigation Property: A Language has many Levels
        public ICollection<Level> Levels { get; set; } = new List<Level>();
    }
}
