using System.ComponentModel.DataAnnotations;

namespace SimplePondresAPI.Models
{
    public class Product
    {
        [Key] public int id { get; set; }
        [Required] public String extreference { get; set; }
        [Required] public int stock { get; set; }
        [Required] public String description { get; set; }
        [Required] public String name { get; set; }
        [Required] public String type { get; set; }
        [Required] public Double price { get; set; }


        public Product()
        {

        }
    }
}
