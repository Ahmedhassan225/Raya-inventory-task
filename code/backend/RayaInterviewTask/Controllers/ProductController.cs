using Application.UnitOfWork.Interfaces;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductUOW _productUOW;
        private readonly ITransactionUOW _transactionUOW;

        public ProductController(IProductUOW productUOW, ITransactionUOW transactionUOW)
        {
            _productUOW = productUOW;
            _transactionUOW = transactionUOW;
        }

        [HttpGet("{id}")]
        public BaseModel<Domain.DTOs.Product.GetProductByIdResponce> GetProductById([FromRoute] long id)
        {
            return _productUOW.GetProductById(id);
        }

        [HttpGet("list")]
        public BaseModel<List<Domain.DTOs.Product.GetProductsResponce>> GetProducts([FromQuery] Domain.DTOs.Product.GetProductsRequest message)
        {
            return _productUOW.GetProducts(message);
        }

        [HttpPost("create")]
        public BaseModel<bool> CreateProduct([FromBody] Domain.DTOs.Product.AddProductRequest message)
        {
            return _productUOW.AddProduct(message);
        }

        [HttpPost("update")]
        public BaseModel<bool> UpdateProduct([FromBody] Domain.DTOs.Product.AddProductRequest message)
        {
            return _productUOW.UpdateProduct(message);
        }

        [HttpPost("delete/{id}")]
        public BaseModel<bool> DeleteProduct([FromRoute] long id)
        {
            return _productUOW.DeleteProductById(id);
        }

        [HttpPost("transaction")]
        public BaseModel<bool> AddTransaction([FromBody] Domain.DTOs.Transaction.AddTransaction message)
        {
            return _transactionUOW.CreateTransaction(message);
        }



    }
}
