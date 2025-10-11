using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_Learning_App.Domain.Entities
{
    public class Level
    {
        public int Id { get; set; }
        public string LevelName { get; set; } = string.Empty;

        // Foreign Key to Language
        public int LanguageId { get; set; }
        // Navigation Property: A Level belongs to one Language
        public Language Language { get; set; } = null!;

        // Navigation Property: A Level has many Stages
        public ICollection<Stage> Stages { get; set; } = new List<Stage>();
    }
}
