using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_SalesDatePrediction.Models
{
    [Table("Employees", Schema = "HR")] // Especifica el esquema en la entidad
    public class Employees
    {
        [Key]
        public int Empid { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Title { get; set; }
        public string Titleofcourtesy { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Hiredate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public int? Mgrid { get; set; }

        // Navigation property for self-referencing foreign key (Manager)
        public Employees Manager { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
