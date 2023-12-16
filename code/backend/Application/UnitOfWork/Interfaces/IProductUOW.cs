using Domain.DTOs.Product;
using Domain.Shared;

namespace Application.UnitOfWork.Interfaces
{
    public interface IProductUOW
    {
        public BaseModel<bool> AddProduct(AddProductRequest message);
        public BaseModel<bool> UpdateProduct(AddProductRequest message);
        public BaseModel<bool> DeleteProductById(long id);
        public BaseModel<GetProductByIdResponce> GetProductById(long id);
        public BaseModel<List<GetProductsResponce>> GetProducts(GetProductsRequest message);
        public byte[] GetProductReportAsPdf(ProductReportRequest message);

    }
}
