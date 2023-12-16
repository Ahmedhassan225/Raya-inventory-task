using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Product
{
    public class GetProductByIdResponce : BaseDTO<GetProductByIdResponce, Domain.Models.Product>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public long CategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public override void AddCustomMappings()
        {
            SetCustomMappingsInverse()
              .Map(dest => dest.Id, src => src.Id)
              .Map(dest => dest.Code, src => src.Code)
              .Map(dest => dest.Name, src => src.Name)
              .Map(dest => dest.Description, src => src.Description)
              .Map(dest => dest.CategoryId, src => src.CategoryId)
              .Map(dest => dest.Quantity, src => src.Quantity)
              .Map(dest => dest.Price, src => src.UnitPrice);

        }
    }
}
