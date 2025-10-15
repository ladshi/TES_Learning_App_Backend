using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES_Learning_App.Domain.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        // We change FullName to a non-identifying Nickname.

        [Required]
        [StringLength(100)]
        public string Nickname { get; set; } = string.Empty;

        // We store Date of Birth to calculate age for content filtering.
        // We must treat this as sensitive data.
        [Required]
        public DateTime DateOfBirth { get; set; }

        // This will store the name of a pre-selected avatar image (e.g., "lion_avatar.png").
        [Required]
        [StringLength(100)]
        public string Avatar { get; set; } = string.Empty;

        [Required]
        [StringLength(10)] // e.g., "en-US", "ta-LK"
        public string NativeLanguageCode { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string TargetLanguageCode { get; set; } = string.Empty;

        // --- The relationship is now different ---
        // A Student profile is created by a User (the Parent).
        // So we rename UserId to ParentUserId to be clear.
        public int ParentUserId { get; set; }
        public User ParentUser { get; set; } = null!;
    }
}

