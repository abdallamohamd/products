using System.ComponentModel.DataAnnotations.Schema;

namespace api.model
{
    public class order
    {
        public int Id { get; set; }

        [ForeignKey("product")]
        public int productid { get; set; }
        public DateTime orederdate { get; set; }
        public int quantity {  get; set; }
        public decimal price { get; set; }
        public product product { get; set; }
    }
}
