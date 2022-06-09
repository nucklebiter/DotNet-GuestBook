using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Models.ViewModels
{
    public class GuestBookVM
    {

        public GuestBook GuestBook { get; set; }
        
        [ValidateNever]
        public IEnumerable<SelectListItem> GuestTypeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> AlienTypeList { get; set; }

    }
}
