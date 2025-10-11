using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_Learning_App.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];

        // Foreign Key to Role
        public int RoleId { get; set; }
        // Navigation Property: A User has one Role
        public Role Role { get; set; } = null!;

        // Navigation Property: A User can have one Student profile
        //public Student? Student { get; set; }
        // A User (Parent) can have a collection of Student (Child) profiles.
        public ICollection<Student> StudentProfiles { get; set; } = new List<Student>();
    }
}
