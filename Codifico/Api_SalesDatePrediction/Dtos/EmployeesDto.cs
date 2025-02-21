namespace Api_SalesDatePrediction.Dtos
{
    public class EmployeesDto
    {
        public int Empid { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        public string FullName => $"{Firstname}  {Lastname}";
    }
}
