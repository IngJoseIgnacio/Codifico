namespace Api_SalesDatePrediction.Dtos
{
    public class OrderDetailDto
    {
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public decimal Discount { get; set; }
    }

}
