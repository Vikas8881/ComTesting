// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Model
{
    [Table("Product Image")]
    public partial class Product_Image
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string? Video { get; set; }
        public int? Pid { get; set; }
    }
}