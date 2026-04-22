using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportsLeague.Domain.Entities
{
    public class Referee : AuditBase
    {
        [MaxLength(100)]
        [Required]
        public required string FirstName { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required]
        public required string LastName { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required]
        public required string Nationality { get; set; } = string.Empty;
    }
}
