using Domain.DTOs.Common;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitOfWork.Interfaces
{
    public interface ILookupUOW
    {
        public BaseModel<List<KVModel>> GetCategory();
    }
}
