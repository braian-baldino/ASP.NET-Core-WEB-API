using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IIncomeRepository : IBaseRepository<Income>
    {
        Task<bool> UpdateBalance(int balanceId);
        Task<IncomeType> GetIncomeCategory(int incomeTypeId);
    }
}
