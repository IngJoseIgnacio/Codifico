namespace Api_SalesDatePrediction.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustID { get; set; }
        public int EmpID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int ShipperID { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }

}
