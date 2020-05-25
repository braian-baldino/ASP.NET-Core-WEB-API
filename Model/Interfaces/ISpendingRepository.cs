using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface ISpendingRepository : IBaseRepository<Spending>
    {
        Task<bool> UpdateBalance(int balanceId);
        Task<SpendingType> GetSpendingCategory(int spendingTypeId);
    }
}
