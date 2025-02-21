using System.ComponentModel.DataAnnotations;

namespace Api_SalesDatePrediction.Models
{
    public class Orders
    {
        [Key]
        public int Orderid { get; set; }
        public int? Custid { get; set; }
        public int Empid { get; set; }
        public DateTime Orderdate { get; set; }
        public DateTime Requireddate { get; set; }
        public DateTime? Shippeddate { get; set; }
        public int Shipperid { get; set; }
        public decimal Freight { get; set; }
        public string Shipname { get; set; }
        public string Shipaddress { get; set; }
        public string Shipcity { get; set; }
        public string Shipregion { get; set; }
        public string Shippostalcode { get; set; }
        public string Shipcountry { get; set; }

        // Navigation properties for foreign keys
        public Customers Customer { get; set; }
        public Employees Employee { get; set; }
        public Shippers Shipper { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
