using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string UserFirstName { get; set; }
        [Required]
        public string UserLastName { get; set; }
        public string? UserStreetAddress { get; set; }
        public string? UserCity { get; set; }
        public string? UserState { get; set; }
        public string? UserPostalCode { get; set; }


    }
}
