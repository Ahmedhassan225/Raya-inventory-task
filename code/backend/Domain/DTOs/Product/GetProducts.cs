using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Product
{
    public class GetProductsRequest : BaseSearchModel
    {
        public string? ProductCode { get; set; }  
        public string? ProductName { get; set; }
        public int? CategoryId { get; set; }
        public ProductsOrderBy OrderBy { get; set; }
    }

    public class GetProductsResponce : BaseDTO<GetProductsResponce, Models.Product>
    {
        public long ProductId { get; set; }
        public string ProductCode {get; set;}
        public string ProductName { get; set; }
        public string CategoryName { get; set;}
        public int ProductQuantaty { get; set; }
        public decimal ProductPrice { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappingsInverse()
                .Map(dest => dest.ProductId, src => src.Id)
                .Map(dest => dest.ProductCode, src => src.Code)
                .Map(dest => dest.ProductName, src => src.Name)
                .Map(dest => dest.CategoryName, src => src.Category.Name)
                .Map(dest => dest.ProductQuantaty, src => src.Quantity)
                .Map(dest => dest.ProductPrice, src => src.UnitPrice);
        }
    }
}
