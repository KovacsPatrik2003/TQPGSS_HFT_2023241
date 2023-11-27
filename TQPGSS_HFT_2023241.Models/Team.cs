using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TQPGSS_HFT_2023241.Models
{
    public class Team
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(Driver))]
        public int Driver1 { get; set; }
        [ForeignKey(nameof(Driver))]
        public int Driver2 { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual List<Driver> Drivers { get; set; }
        public int Points { get; set; }
        public string Principal { get; set; }
    }
}
