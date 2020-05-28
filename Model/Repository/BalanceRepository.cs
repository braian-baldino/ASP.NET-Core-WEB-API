using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly DataContext _context;
        public BalanceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Balance> Add(Balance entity,int userId)
        {
            try
            {
                if (entity == null)
                {
                    return null;
                }

                entity.UserId = userId;
                entity.TotalIncomes = 0;
                entity.TotalSpendings = 0;
                entity.Result = 0;

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Balance> Delete(int id)
        {
            try
            {
                var balance = await _context.Balances.Where(b => b.Id == id).FirstOrDefaultAsync();

                if (balance == null)
                    return null;

                _context.Remove(balance);
                await _context.SaveChangesAsync();

                return balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Balance> Get(int id,int userId)
        {
            try
            {
                var balance = await _context.Balances
                    .Include(i => i.Incomes)
                    .ThenInclude(c => c.Category)
                    .Include(s => s.Spendings)
                    .ThenInclude(c => c.Category)
                    .Include(m => m.Month)
                    .Where(b => b.Id == id && b.UserId == userId)
                    .FirstOrDefaultAsync();

                return balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Balance>> GetAll(int userId)
        {
            try
            {
                var balances = await _context.Balances
                    .Include(m => m.Month)
                    .Where(b => b.UserId == userId)
                    .OrderBy(b => b.Month.Id)
                    .ToListAsync();

                return balances;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Balance> Update(Balance entity)
        {
            try
            {
                if (entity == null)
                    return null;

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Exist (int id)
        {
            return _context.Balances.Any(e => e.Id == id);
        }

        public async Task<List<Month>> GetAllMonths()
        {
            try
            {
                var months = await _context.Months
                    .OrderBy(m => m.Id)
                    .ToListAsync();
                return months;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Month> GetMonth(int monthId)
        {
            try
            {
                var month = await _context.Months.Where(m => m.Id == monthId).FirstOrDefaultAsync();

                if(month == null)
                {
                    return null;
                }

                return month;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Calculate(int balanceId)
        {
            try
            {
                var balance = await _context.Balances
                    .Include(i => i.Incomes)
                    .Include(s => s.Spendings)
                    .Where(b => b.Id == balanceId).FirstOrDefaultAsync();

                if(balance == null)
                {
                    return false;
                }

                balance.TotalIncomes = 0;
                balance.TotalSpendings = 0;

                foreach (Income income in balance.Incomes)
                {
                    balance.TotalIncomes += income.Amount;
                }

                foreach (Spending spending in balance.Spendings)
                {
                    balance.TotalSpendings += spending.Amount;
                }

                balance.Result = balance.TotalIncomes - balance.TotalSpendings;

                await Update(balance);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User> ValidUser(int userId)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Balance>> BalancesFromYear(int anualBalanceId, int userId)
        {
            try
            {
                var balancesFromYear = await _context.Balances
                    .Where(b => b.AnualBalanceId == anualBalanceId && b.UserId == userId)
                    .Include(m => m.Month)
                    .OrderBy(m => m.Id)
                    .ToListAsync();

                return balancesFromYear;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    
}
