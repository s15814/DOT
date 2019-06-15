using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGameApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Prosze podać login.")]
        [EmailAddress(ErrorMessage = "E-mail ma niepoprawny format.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę podać hasło.")]
        [StringLength(100, ErrorMessage = "{0} musi mieć przynajmniej {2} znaków długości.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Niezgodne hasła.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [StringLength(70, ErrorMessage = Notifications.MAX_LEN)]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(70, ErrorMessage = Notifications.MAX_LEN)]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
    }
}