using Application.Repository.Interfaces;
using Application.Repository;
using Application.UnitOfWork.Interfaces;
using Domain.Shared;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Infrastructure.Middleware.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Domain;
using Mapster;
using Domain.DTOs.Product;
using Domain.DTOs.Transaction;
using Application.Helpers.Interfaces;
using Application.Helpers;

namespace Application.UnitOfWork
{
    public class ProductUOW : IProductUOW
    {

        #region DataMember
        protected IRepositoryProvider RepositoryProvider { get; set; }
        protected IRepository<Product> ProductRepo { get { return GetStandardRepo<Product>(); } }
        protected IRepository<Category> CategoryRepo { get { return GetStandardRepo<Category>(); } }

        private ILogger<ProductUOW> _logger;

        private readonly ITransactionUOW _transactionUOW;
        private readonly IPdfHelper _pdfhelper;

        #endregion

        #region Constuctor
        public ProductUOW(IRepositoryProvider repositoryProvider, ILogger<ProductUOW> logger, ITransactionUOW transactionUOW, IPdfHelper pdfhelper)
        {
            if (repositoryProvider.DbContext == null) 
                throw new ArgumentNullException("dbContext is null"); /// if Database context not initalized Through Exception
            
            this.RepositoryProvider = repositoryProvider;
            _logger = logger;
            _transactionUOW = transactionUOW;
            _pdfhelper = pdfhelper;
        }
        #endregion

        #region Public Methods
        public BaseModel<bool> AddProduct(AddProductRequest message)
        {
            if(message== null) 
                throw new ArgumentNullException("request");

            if(string.IsNullOrEmpty(message.ProductName))
                throw new ArgumentNullException("ProductName");

            if (string.IsNullOrEmpty(message.ProductCode))
                throw new ArgumentNullException("ProductCode");

            if (message.ProductQuantity < 0)
                throw new CustomException("Prodcuct Quantity Can't be less than 0", statusCode: System.Net.HttpStatusCode.BadRequest);

            if(message.ProductPrice < 0)
                throw new CustomException("Prodcuct UnitPrice Can't be less than 0", statusCode: System.Net.HttpStatusCode.BadRequest);

            var category = CategoryRepo.FindById(message.ProductCategoryId);
            if(category is null)
                throw new CustomException($"Id {message.ProductCategoryId} is not Valid Category Id", statusCode: System.Net.HttpStatusCode.BadRequest);

            var isCodeExists = ProductRepo.Where(p => p.Code == message.ProductCode ).Any();
            if(isCodeExists)
                throw new CustomException("Prodcuct Code Already Exists", statusCode: System.Net.HttpStatusCode.BadRequest);

            var entity = message.ToEntity();

            ProductRepo.Add(entity);
            var isProductSaved = ProductRepo.SaveChanges() > 0;

            if(isProductSaved && message.ProductQuantity > 0)
            {
                var transaction = new AddTransaction()
                {
                    ProductId = entity.Id,
                    Quantity = message.ProductQuantity,
                    Type = TransactionType.Purchase
                };

                var isTransactionCreated = _transactionUOW.CreateTransaction(transaction).Result;
                return new BaseModel<bool>(isTransactionCreated);

            }

            return new BaseModel<bool>(isProductSaved);


        }
        public BaseModel<bool> DeleteProductById(long id)
        {
            var entity = ProductRepo.FindById(id);
            if (entity is null)
                throw new NotFoundException($"Product with Id {id} is not Found");

            ProductRepo.Delete(entity);
            var result = ProductRepo.SaveChanges();

            return new BaseModel<bool>(result > 0);
        }
        public BaseModel<GetProductByIdResponce> GetProductById(long id)
        {
            var entity = ProductRepo.FindById(id);
            if (entity is null)
                throw new NotFoundException($"Product with Id {id} is not Found");

            var responce = GetProductByIdResponce.FromEntity(entity);

            return new BaseModel<GetProductByIdResponce>(responce);
        }
        public BaseModel<List<GetProductsResponce>> GetProducts(GetProductsRequest message)
        {
            var query = ProductRepo.All()
                .Include(pr => pr.Category)
                .Where(pr =>
                    (String.IsNullOrWhiteSpace(message.ProductCode) || pr.Code.Contains(message.ProductCode))
                    && (String.IsNullOrWhiteSpace(message.ProductName) || pr.Name.Contains(message.ProductName))
                    && (!message.CategoryId.HasValue || message.CategoryId == pr.CategoryId))
                .AsSplitQuery();

            query = message.OrderBy == ProductsOrderBy.Name ? query.OrderBy(q => q.Name) 
                    : message.OrderBy == ProductsOrderBy.Price ? query.OrderBy(q => q.UnitPrice)
                    : message.OrderBy == ProductsOrderBy.Quantity ? query.OrderBy(q => q.Quantity)
                    : query.OrderBy(q => q.Name);

            int count = query.Count();

            var result = query
                .Skip((message.PageIndex) * message.PageSize)
                .Take(message.PageSize)
                .ToList();

            List<GetProductsResponce> responce = new();
            result.ForEach(item => responce.Add(GetProductsResponce.FromEntity(item)));

            return new BaseModel<List<GetProductsResponce>>(responce,
                new PageInfoAppModel(message.PageIndex, count, message.PageSize));
        }
        public BaseModel<bool> UpdateProduct(AddProductRequest message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (string.IsNullOrEmpty(message.ProductName))
                throw new ArgumentNullException("ProductName");

            if (string.IsNullOrEmpty(message.ProductCode))
                throw new ArgumentNullException("ProductCode");

            if (message.ProductPrice < 0)
                throw new CustomException("Prodcuct UnitPrice Can't be less than 0", statusCode: System.Net.HttpStatusCode.BadRequest);

            var category = CategoryRepo.FindById(message.ProductCategoryId);
            if (category is null)
                throw new CustomException($"Id {message.ProductCategoryId} is not Valid Category Id", statusCode: System.Net.HttpStatusCode.BadRequest);

            var isCodeExists = ProductRepo.Where(p => p.Code == message.ProductCode && p.Id != message.ProductId).Any();
            if (isCodeExists)
                throw new CustomException("Prodcuct Code Already Exists", statusCode: System.Net.HttpStatusCode.BadRequest);

            var entity = ProductRepo.FindById(message.ProductId);

            entity.Code = message.ProductCode;
            entity.Name = message.ProductName;
            entity.Description = message.ProductDescription;
            entity.UnitPrice = message.ProductPrice;
            entity.CategoryId = (int)message.ProductCategoryId;

            ProductRepo.Update(entity);
            var result = ProductRepo.SaveChanges();

            return new BaseModel<bool>(result > 0);
        }
        public byte[] GetProductReportAsPdf(ProductReportRequest message)
        {
            var query = ProductRepo.All()
              .Include(pr => pr.Category)
              .Where(p =>(!message.CategoryId.HasValue || message.CategoryId == p.CategoryId))
              .AsSplitQuery();

            query = message.OrderBy == ProductsOrderBy.Name ? query.OrderBy(q => q.Name)
                : message.OrderBy == ProductsOrderBy.Price ? query.OrderBy(q => q.UnitPrice)
                : message.OrderBy == ProductsOrderBy.Quantity ? query.OrderBy(q => q.Quantity)
                : query.OrderBy(q => q.Name);

            var result = query.ToList();

            List<ProductReportResponce> dataSource = new();
            result.ForEach(item => dataSource.Add(ProductReportResponce.FromEntity(item)));

            var html = _pdfhelper.GenerateHtmlTable(dataSource, "Inventory Products");
            var pdf = _pdfhelper.ConvertHtmlToPdf(html);

            return pdf;
        }

        #endregion

        #region Private Methods
        private IRepository<T> GetStandardRepo<T>() where T : BaseEntity
        {
            try
            {
                var repo = RepositoryProvider.GetRepositoryForEntityType<T>();
                return repo;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw ex;
            }
        }


        #endregion

    }
}
