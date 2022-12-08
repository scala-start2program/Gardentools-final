using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Gardentools.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [Display(Name = "Klant")]
        public User User { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime DateTimeStamp { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

    }
}
