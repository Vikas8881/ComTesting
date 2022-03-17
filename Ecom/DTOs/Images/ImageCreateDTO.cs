using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Ecommerce.DTOs.Images
{
    public class ImageCreateDTO:BaseDTO
    {
       
        public string? Image { get; set; }

        public string? Video { get; set; }
       
        public int? Pid { get; set; }
        public string? ImageData { get; set; }

        public string? OriginalImageName { get; set; }
    }
}
