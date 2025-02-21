using System.ComponentModel.DataAnnotations;

namespace Api_SalesDatePrediction.Models
{
    public class OrderDetails
    {
        public int Orderid { get; set; }
        public int Productid { get; set; }
        public decimal Unitprice { get; set; }
        public short Qty { get; set; }
        public decimal Discount { get; set; }

        // Navigation properties for foreign keys
        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
