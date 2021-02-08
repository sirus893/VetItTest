using System;
using System.Collections.Generic;

#nullable disable

namespace VetIt.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? InactiveDate { get; set; }
        public int OrganisationId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string SupplierProductCode { get; set; }
        public decimal SupplierPrice { get; set; }
        public string ManufacturerCode { get; set; }
        public decimal BoughtInQuantity { get; set; }
        public decimal SoldInQuantity { get; set; }
        public decimal SoldInMarkup { get; set; }
        public decimal? WholesaleDiscount { get; set; }
        public decimal? ManufacturerDiscount { get; set; }
        public bool UseWholesaleDiscount { get; set; }
        public bool UseManufacturerDiscount { get; set; }
        public bool? Dangerous { get; set; }
        public bool Neutering { get; set; }
        public bool Euthanasia { get; set; }
        public bool? BookWithoutServiceFee { get; set; }
        public bool? PrescriptionOnly { get; set; }
    }
}
