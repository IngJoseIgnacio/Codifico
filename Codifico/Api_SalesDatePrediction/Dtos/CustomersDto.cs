namespace Api_SalesDatePrediction.Dtos
{
    public class CustomersDto
    {
        public string CustomerName { get; set; }
        public DateTime LastOrderdate { get; set; }
        public DateTime NextPredictedOrder { get; set; }
    }
}
