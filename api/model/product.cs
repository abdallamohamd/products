using System.ComponentModel.DataAnnotations.Schema;

namespace api.model
{
    public class product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        [ForeignKey("supplier")]
        public int SupplierId { get; set; }
        public supplier? supplier { get; set; }

        
    }
}
