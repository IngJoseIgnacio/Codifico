using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_SalesDatePrediction.Models
{
    [Table("Products", Schema = "Production")] // Especifica el esquema en la entidad
    public class Products
    {
        [Key]
        public int Productid { get; set; }
        public string Productname { get; set; }
        public int Supplierid { get; set; }
        public int Categoryid { get; set; }
        public decimal Unitprice { get; set; }
        public bool Discontinued { get; set; }

        // Navigation properties for foreign keys
        public Suppliers Supplier { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
