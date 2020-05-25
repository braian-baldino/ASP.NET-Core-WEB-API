using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IAnualBalanceRepository : IBaseRepository<AnualBalance>
    {
        Task<bool> AddMonths(int anualBalanceId);
        Task<bool> UniqueYear(int year);
    }
}
