using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DTOs.Product
{
    public class ProductCreateDTO:BaseDTO
    {
       
      
        [Column("Product Name")]
        [Required]
        public string Product_Name { get; set; }
        [Column("Product Description")]
        [Required]
        public string Product_Description { get; set; }
        [Required]
        public string Unit { get; set; }
        [Column("Minimum Purchase")]
        [Required]
        public int? Minimum_Purchase { get; set; }
       
        public string Tags { get; set; }
        [Required]
        public bool? Refundable { get; set; }

        [Column("Purchase Price")]
        [Required]
        public int? Purchase_Price { get; set; }
        [Column("Sale Price")]
        [Required]
        public int? Sale_Price { get; set; }
        [Column("Old Price")]

        public int? Old_Price { get; set; }
      
        [Required(ErrorMessage = "Please Choose Category")]
        public string? Cat_ID { get; set; }
        [Column("Product Code")]
        [StringLength(50)]
        public string Product_Code { get; set; }
        [Column("Minimum Stock")]
        [Required]
        public int? Minimum_Stock { get; set; }
        public string? thumbnail { get; set; }

        public string? ImageData { get; set; }

        public string? OriginalImageName { get; set; }

    }
}
