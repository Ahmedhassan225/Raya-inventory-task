using Application.Repository.Interfaces;
using Application.Repository;
using Application.UnitOfWork.Interfaces;
using Domain.DTOs.Common;
using Domain.Shared;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.UnitOfWork
{
    public class LookupUOW : ILookupUOW
    {
        #region DataMember
        protected IRepositoryProvider RepositoryProvider { get; set; }
        protected IRepository<Category> CategoryRepo { get { return GetStandardRepo<Category>(); } }

        private ILogger<LookupUOW> _logger;

        #endregion

        #region Constuctor
        public LookupUOW(IRepositoryProvider repositoryProvider, ILogger<LookupUOW> logger)
        {
            if (repositoryProvider.DbContext == null)
                throw new ArgumentNullException("dbContext is null"); /// if Database context not initalized Through Exception

            this.RepositoryProvider = repositoryProvider;
            _logger = logger;

        }
        #endregion
        public BaseModel<List<KVModel>> GetCategory()
        {
           var result = CategoryRepo.All()
                .Select(c => new KVModel() { Id = c.Id, Name = c.Name })
                .ToList();

            return new BaseModel<List<KVModel>>(result);
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
