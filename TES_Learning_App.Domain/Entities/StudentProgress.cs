using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_Learning_App.Domain.Entities
{
    public class StudentProgress
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key to User
        // Progress is tracked for a specific Student (child) profile
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        // Foreign Key to Activity
        public int ActivityId { get; set; }
        // Navigation Property
        public Activity Activity { get; set; } = null!;
    }
}
