using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public class GuestBook
    {

        public int Id { get; set; }
        public string GuestBookFirstName { get; set; }
        public string GuestBookLastName { get; set; }
        public string GuestBookEmail { get; set; }
        public string GuestBookDescription { get; set; }
        public DateTime GuestBookCreatedDateTime { get; set; } = DateTime.Now;
        [Required]
        public int GuestTypeId { get; set; }
        [ForeignKey("GuestTypeId")]
        [ValidateNever]
        public GuestType GuestType { get; set; }
        [Required]
        public int AlienTypeId { get; set; }
        [ForeignKey("AlienTypeId")]
        [ValidateNever]
        public AlienType AlienType { get; set; }

    }
}
