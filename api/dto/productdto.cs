using System.ComponentModel.DataAnnotations.Schema;

namespace api.dto
{
    public class productdto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int SupplierId { get; set; }

    } 

}
