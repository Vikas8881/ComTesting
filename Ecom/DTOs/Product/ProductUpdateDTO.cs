using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DTOs.Product
{
    public class ProductUpdateDTO:BaseDTO
    {
        [Column("Product Name")]
        [Required]
        public string Product_Name { get; set; }
        [Column("Product Description")]
        [Required]
        public string Product_Description { get; set; }
        [Column("Product Price")]
        public int? Product_Price { get; set; }
        public int? Quantity { get; set; }
        public int? Cat_ID { get; set; }
    }
}
