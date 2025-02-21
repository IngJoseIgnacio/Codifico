using System.ComponentModel.DataAnnotations;

namespace Api_SalesDatePrediction.Models
{
    public class Category
    {
        [Key]
        public int Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Description { get; set; }

        // Navigation property for related products
        public ICollection<Products> Products { get; set; }
    }
}
