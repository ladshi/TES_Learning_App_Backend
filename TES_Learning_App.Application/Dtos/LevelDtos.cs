using System.ComponentModel.DataAnnotations;

namespace TES_Learning_App.Application.Dtos
{
    public class CreateLevelDTO
    {
        [Required]
        [StringLength(100)]
        public string LevelName { get; set; } = string.Empty;
        
        [Required]
        public int LanguageId { get; set; }
    }

    public class UpdateLevelDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string LevelName { get; set; } = string.Empty;
        
        [Required]
        public int LanguageId { get; set; }
    }

    public class LevelResponseDTO
    {
        public int Id { get; set; }
        public string LevelName { get; set; } = string.Empty;
        public int LanguageId { get; set; }
        public string? LanguageName { get; set; }
    }
}
