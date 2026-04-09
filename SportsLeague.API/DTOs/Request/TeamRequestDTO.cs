using System.ComponentModel.DataAnnotations;

namespace Sportsleague.API.DTOs.Request
{
    public class TeamRequestDTO
    {
        [MaxLength(100)]
        [Required]
        public required string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required]
        public required string City { get; set; } = string.Empty;
        [MaxLength(150)]
        public string Stadium { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? LogoUrl { get; set; }
        public DateTime FoundedDate { get; set; }
    }
}
