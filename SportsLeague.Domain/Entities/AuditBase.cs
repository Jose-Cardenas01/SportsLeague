using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace SportsLeague.Domain.Entities
{
    public abstract class AuditBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
