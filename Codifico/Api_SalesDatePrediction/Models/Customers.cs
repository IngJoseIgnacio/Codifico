using System.ComponentModel.DataAnnotations;

namespace Api_SalesDatePrediction.Models
{
    public class Customers
    {
        [Key]
        public int Custid { get; set; }
        public string Companyname { get; set; }
        public string Contactname { get; set; }
        public string Contacttitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        // Navigation property for related orders
        public ICollection<Orders> Orders { get; set; }
    }
}
