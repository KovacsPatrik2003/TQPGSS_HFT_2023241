using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQPGSS_HFT_2023241.Models
{
    public class GrandPrix
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        [NotMapped]
        public virtual Driver Driver { get; set; }
        [ForeignKey(nameof(Driver))]
        public int WhoWon { get; set; }
    }
}
