// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model
{
    public partial class SP_GetViewimageRecordResult
    {
        [Column("Category Name")]
        public string CategoryName { get; set; }
        public int ID { get; set; }
        [Column("Product Name")]
        public string ProductName { get; set; }
        [Column("Product Description")]
        public string ProductDescription { get; set; }
        public string Unit { get; set; }
        [Column("Minimum Purchase")]
        public int? MinimumPurchase { get; set; }
        public string Tags { get; set; }
        public bool? Refundable { get; set; }
        [Column("Purchase Price")]
        public int? PurchasePrice { get; set; }
        [Column("Sale Price")]
        public int? SalePrice { get; set; }
        [Column("Old Price")]
        public int? OldPrice { get; set; }
        public string Cat_ID { get; set; }
        [Column("Product Code")]
        public string ProductCode { get; set; }
        [Column("Minimum Stock")]
        public int? MinimumStock { get; set; }
        public string thumbnail { get; set; }
    }
}
