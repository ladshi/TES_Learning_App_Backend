using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_Learning_App.Domain.Entities
{
    public class MainActivity
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty; // e.g., "Speaking"

        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
