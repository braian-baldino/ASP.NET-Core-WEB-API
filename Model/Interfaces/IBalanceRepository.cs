using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IBalanceRepository : IBaseRepository<Balance>
    {
        Task<List<Balance>> BalancesFromYear(int anualBalanceId,int userId);
        Task<bool> Calculate(int balanceId);
        Task<List<Month>> GetAllMonths();
        Task<Month> GetMonth(int monthId);
    }
}
