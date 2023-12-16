using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Product
{
    public class ProductReportRequest
    {
        public int? CategoryId { get; set; }
        public ProductsOrderBy OrderBy { get; set; }
    }

    public class ProductReportResponce : BaseDTO<ProductReportResponce, Models.Product>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantaty { get; set; }
        public decimal Price { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappingsInverse()
                .Map(dest => dest.Code, src => src.Code)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Category, src => src.Category.Name)
                .Map(dest => dest.Quantaty, src => src.Quantity)
                .Map(dest => dest.Price, src => src.UnitPrice);
        }
    }
}
