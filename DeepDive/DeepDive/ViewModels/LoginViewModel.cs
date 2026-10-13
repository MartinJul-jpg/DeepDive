using System.ComponentModel.DataAnnotations;

namespace DeepDive.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "E-mail skal udfyldes.")]
        [EmailAddress(ErrorMessage = "Ugyldig e-mailadresse.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adgangskode skal udfyldes.")]
        public string Password { get; set; } = string.Empty;

    }
}
