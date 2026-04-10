using SportsLeague.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Sportsleague.API.DTOs.Request
{
    public class PlayerRequestDTO
    {
        [MaxLength(100)]
        [Required]
        public required string FirstName { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required]
        public required string LastName { get; set; } = string.Empty;
        public required DateTime BirthDate { get; set; }
        public required int Number { get; set; }
        public required PlayerPosition Position { get; set; }
        // Foreign Key
        public int TeamId { get; set; }
    }
}
