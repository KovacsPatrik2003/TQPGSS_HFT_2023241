using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TQPGSS_HFT_2023241.Models
{
    public class Driver
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Points { get; set; }
        [NotMapped]
        public virtual Team team { get; set; }
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
    }
}
