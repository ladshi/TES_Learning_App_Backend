using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_Learning_App.Domain.Entities
{
    public class ActivityType
    {
        public int Id { get; set; } // Primary Key
        public string TypeName { get; set; } = string.Empty; // e.g., "Flashcards"
        public string? Description { get; set; }

        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
