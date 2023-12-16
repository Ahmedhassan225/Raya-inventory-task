using Application.UnitOfWork.Interfaces;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class LookupController : BaseApiController
    {
        private readonly ILookupUOW _lookupUOW;

        public LookupController(ILookupUOW lookupUOW)
        {
            _lookupUOW = lookupUOW;
        }

        [HttpGet("category")]
        public BaseModel<List<Domain.DTOs.Common.KVModel>> GetCategory()
        {
            return _lookupUOW.GetCategory();
        }
    }
}
