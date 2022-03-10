using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DTOs.Images
{
    public class ImageReadDTO:BaseDTO
    {
        public string Product_Image1 { get; set; }
        [Column("Video Path")]
        public string Video_Path { get; set; }
        public int? Pid { get; set; }
    }
}
