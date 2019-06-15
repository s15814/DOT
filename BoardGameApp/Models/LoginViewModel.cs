using System.ComponentModel.DataAnnotations;

namespace BoardGameApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Proszę podać adres email.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Adres email ma niepoprawny format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Proszę podać hasło.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}