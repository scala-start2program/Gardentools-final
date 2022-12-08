using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using System.Xml.Linq;

namespace Gardentools.Models
{
    [Index(nameof(User.Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail is vereist")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is niet geldig")]
        public string Email { get; set; }

        [Display(Name = "Acthernaam")]
        [Required(ErrorMessage = "Acthernaam is vereist")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimaal 2, maximaal 50 letters")]
        public string Name { get; set; }

        [Display(Name = "Voornaam")]
        [Required(ErrorMessage = "Voornaam is vereist")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimaal 2, maximaal 50 letters")]
        public string FirstName { get; set; }

        [Display(Name = "Paswoord")]
        [Required(ErrorMessage = "Paswoord is vereist")]
        [StringLength(250, MinimumLength = 8, ErrorMessage = "Minimaal 8 karakters")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Minstens 1 kleine letter, 1 hoofdletter en 1 cijfer")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Adres")]
        [StringLength(100, ErrorMessage = "Maximaal 100 letters")]
        public string Address { get; set; }

        [Display(Name = "Postnr")]
        [StringLength(10, ErrorMessage = "Maximaal 10 letters")]
        public string PostalCode { get; set; }

        [Display(Name = "Gemeente")]
        [StringLength(100, ErrorMessage = "Maximaal 100 letters")]
        public string City { get; set; }

        [Display(Name = "Land")]
        [StringLength(100, ErrorMessage = "Maximaal 100 letters")]
        public string Country { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
