using System.ComponentModel.DataAnnotations;

namespace SimplePondresAPI.Models
{
    public class Order
    {
        [Key] public int id { get; set; }
        [Required] public string state { get; set; }
        [Required] public DateTime deliverydate { get; set; }
        [Required] public string address { get; set; }
        [Required] public string productreference { get; set; }
        public string requirements { get; set; }
        public string companyname { get; set; }


        public Order()
        {
            
        }

    }
}
