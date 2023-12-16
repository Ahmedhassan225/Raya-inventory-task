using Domain.DTOs.Transaction;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitOfWork.Interfaces
{
    public interface ITransactionUOW
    {
        public BaseModel<bool> CreateTransaction(AddTransaction message);
    }
}
