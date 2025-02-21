namespace Api_SalesDatePrediction.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }  // Primary Key
        public int OrderID { get; set; }  // Foreign Key
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public decimal Discount { get; set; }

        public Order Order { get; set; }  // Navigation Property
    }

}
