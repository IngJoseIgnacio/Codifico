namespace Api_SalesDatePrediction.Dtos
{
    public class OrdersCustomersDto
    {
        public int Custid { get; set; }
        public string Companyname { get; set; }
        public int OrderId { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
    }
}
