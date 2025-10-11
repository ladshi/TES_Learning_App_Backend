using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_Learning_App.Domain.Entities
{
    public class Stage
    {
        public int Id { get; set; }
        public string StageName { get; set; } = string.Empty;

        // Foreign Key to Level
        public int LevelId { get; set; }
        // Navigation Property: A Stage belongs to one Level
        public Level Level { get; set; } = null!;

        // Navigation Property: A Stage has many Activities
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}