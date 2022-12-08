using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Gardentools.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Categorie")]
        [Required(ErrorMessage = "Naam categorie is vereist")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Minimaal 2, maximaal 30 letters")]
        public string CategoryName { get; set; }
    }
}
