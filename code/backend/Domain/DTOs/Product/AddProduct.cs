using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Product
{
    public class AddProductRequest : BaseDTO<AddProductRequest, Domain.Models.Product>
    {
        public long? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductDescription { get; set; } = string.Empty;
        public int? ProductCategoryId { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(dest => dest.Name, src => src.ProductName)
                .Map(dest => dest.Description, src => src.ProductDescription)
                .Map(dest => dest.Code, src => src.ProductCode)
                .Map(dest => dest.Quantity, src => src.ProductQuantity)
                .Map(dest => dest.UnitPrice, src => src.ProductPrice)
                .Map(dest => dest.CategoryId, src => src.ProductCategoryId);
        }
    }
}
