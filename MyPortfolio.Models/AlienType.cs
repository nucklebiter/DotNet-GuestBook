using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public class AlienType
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Alien Type Name")]
        public string AlienTypeName { get; set; }
        [Display(Name = "Alien Type Created")]
        public DateTime AlienTypeCreatedDateTime { get; set; }

    }
}
