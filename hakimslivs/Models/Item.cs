using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hakimslivs.Models
{
    public class Item
    {
        public int ID { get; set; }
        public Category? Category { get; set; }

        [MaxLength(50)]
        public string Product { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

    }

    public enum Category
    {
        Frukt,
        Grönsaker,
        Dryck,
        Bröd,
        Godis
    }
}
