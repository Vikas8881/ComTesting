using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DTOs.Product
{
    public class ProductReadDTO:BaseDTO
    {
       
        [Column("Product Name")]
        public string Product_Name { get; set; }
        [Column("Product Description")]
        public string Product_Description { get; set; }
        [Column("Product Price")]
        public int? Product_Price { get; set; }
        public int? Quantity { get; set; }
        public string? Cat_ID { get; set; }
    }
}
