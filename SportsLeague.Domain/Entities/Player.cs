using SportsLeague.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportsLeague.Domain.Entities
{
    public class Player : AuditBase
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
        // Navigation Property
        public Teams Team { get; set; } = null!;
    }
}
