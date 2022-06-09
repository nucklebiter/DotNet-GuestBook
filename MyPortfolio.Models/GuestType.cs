using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models
{
    public class GuestType
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Guest Type Name")]
        public string GuestTypeName { get; set; }
        [Display(Name = "Guest Type Created")]
        public DateTime GuestTypeCreatedDateTime { get; set; } = DateTime.Now;

    }
}
