using Domain.Shared;

namespace Domain.DTOs.Transaction
{
    public class AddTransaction : BaseDTO<AddTransaction, Models.InventoryTransaction>
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public TransactionType Type { get; set; }
        public override void AddCustomMappings()
        {
            SetCustomMappings()
               .Map(dest => dest.ProductId, src => src.ProductId)
               .Map(dest => dest.Quantity, src => src.Quantity)
               .Map(dest => dest.Type, src => src.Type);
        }

    }
}
