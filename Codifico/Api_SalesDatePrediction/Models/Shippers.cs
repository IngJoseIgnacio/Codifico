using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_SalesDatePrediction.Models
{
    [Table("Employees", Schema = "Sales")] // Especifica el esquema en la entidad
    public class Shippers
    {
        [Key]
        public int Shipperid { get; set; }
        public string Companyname { get; set; }
        public string Phone { get; set; }

        // Navigation property for related orders
        public ICollection<Orders> Orders { get; set; }
    }
}
