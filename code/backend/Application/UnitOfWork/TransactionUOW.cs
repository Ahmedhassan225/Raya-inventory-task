using Application.Repository.Interfaces;
using Application.UnitOfWork.Interfaces;
using Domain.DTOs.Transaction;
using Domain.Models;
using Domain.Shared;
using Infrastructure.Middleware.Exceptions.Common;
using Microsoft.Extensions.Logging;

namespace Application.UnitOfWork
{
    public class TransactionUOW : ITransactionUOW
    {
        #region DataMember
        protected IRepositoryProvider RepositoryProvider { get; set; }
        protected IRepository<Product> ProductRepo { get { return GetStandardRepo<Product>(); } }
        protected IRepository<InventoryTransaction> TransactionRepo { get { return GetStandardRepo<InventoryTransaction>(); } }

        private ILogger<TransactionUOW> _logger;

        #endregion

        #region Constuctor
        public TransactionUOW(IRepositoryProvider repositoryProvider, ILogger<TransactionUOW> logger)
        {
            if (repositoryProvider.DbContext == null)
                throw new ArgumentNullException("dbContext is null"); /// if Database context not initalized Through Exception

            this.RepositoryProvider = repositoryProvider;
            _logger = logger;

        }

        #endregion
        public BaseModel<bool> CreateTransaction(AddTransaction message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            var product = ProductRepo.FindById(message.ProductId);
            if (product == null)
                throw new NotFoundException($"Product with Id {message.ProductId} is not Found");

            if (message.Type == Domain.TransactionType.Sale && product.Quantity < message.Quantity)
                throw new CustomException($"insufficient inventory for Product id {message.ProductId} ");


            using (var dbContextTransaction = this.RepositoryProvider.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var entity = message.ToEntity();
                    entity.Price = product.UnitPrice * entity.Quantity;

                    TransactionRepo.Add(entity);
                    var result = TransactionRepo.SaveChanges();

                    if (message.Type == Domain.TransactionType.Purchase)
                        product.Quantity += message.Quantity;
                    else if (message.Type == Domain.TransactionType.Sale)
                        product.Quantity -= message.Quantity;
                    else
                        throw new CustomException("TransactionType not Handled", statusCode: System.Net.HttpStatusCode.NotFound);

                    ProductRepo.Update(product);
                    ProductRepo.SaveChanges();

                    dbContextTransaction.Commit();
                    return new BaseModel<bool>(result > 0);

                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    throw ex;
                }
            }
        }


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
