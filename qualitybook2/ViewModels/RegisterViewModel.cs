using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Home Number")]
        public string HomeNumber { get; set; }

        [Required]
        [Display(Name = "Work Number")]
        public string WorkNumber { get; set; }

        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        public string ReturnUrl { get; set; }
    }
}
