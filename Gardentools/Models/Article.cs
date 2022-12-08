using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.ComponentModel;

namespace Gardentools.Models
{
    public class Article
    {
        public int Id { get; set; }

        [ForeignKey("Brand")]
        [Display(Name = "Merk")]
        public int BrandId { get; set; }

        [Display(Name = "Merk")]
        public Brand Brand { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Categorie")]
        public int CategoryId { get; set; }

        [Display(Name = "Categorie")]
        public Category Category { get; set; }

        [Display(Name = "Artikelnaam")]
        [Required(ErrorMessage = "Geef een artikel(naam) op !")]
        [StringLength(30, ErrorMessage = "Artikelnaam maximaal 30 letters")]
        public string ArticleName { get; set; }

        [Display(Name = "Prijs")]
        [Required(ErrorMessage = "Voer een waarde in tussen 1 en 20000")]
        [Range(1, 20000, ErrorMessage = "Kies een waarde tussen 1 en 20000")]
        [Column(TypeName = "decimal(8, 2)")]
        [DefaultValue(0)]
        public decimal Price { get; set; } = 0M;

        [Display(Name = "Omschrijving")]
        [StringLength(500, ErrorMessage = "Omschrijving maximaal 500 letters")]
        public string Description { get; set; }

        [Display(Name = "Aandrijving")]
        public string EnergySupply { get; set; }

        [Display(Name = "Garantie")]
        [Required(ErrorMessage = "Voer een waarde in tussen 1 en 5 !")]
        [Range(1, 5, ErrorMessage = "Kies een waarde tussen 1 en 5")]
        [DefaultValue(2)]
        public int Warranty { get; set; } = 2;

        public string ImagePath { get; set; }

        public double Score { get; set; } = 5.0;

    }
}
