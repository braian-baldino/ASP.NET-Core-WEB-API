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
    public class AnualBalanceRepository : IAnualBalanceRepository
    {
        private readonly DataContext _context;
        private readonly IBalanceRepository _balanceRepository;

        public AnualBalanceRepository(DataContext context,IBalanceRepository balanceRepository)
        {
            _context = context;
            _balanceRepository = balanceRepository;
        }

        public async Task<AnualBalance> Add(AnualBalance anualBalance,int userId)
        {
            try
            {
                //Validates model.
                if (anualBalance == null)
                {
                    return null;
                }

                //Assign the validated user id to the Anual Balance
                anualBalance.UserId = userId;

                if(! await UniqueYear(userId,anualBalance.Year))
                {
                    return null;
                }

                //Initialize the anual balance result.
                anualBalance.Result = 0;
                anualBalance.Positive = true;

                await _context.AddAsync(anualBalance);
                await _context.SaveChangesAsync();

                return anualBalance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AnualBalance> Delete(int id)
        {
            try
            {
                var anualBalance = await _context.AnualBalances.Where(b => b.Id == id).FirstOrDefaultAsync();

                if (anualBalance == null)
                    return null;

                _context.Remove(anualBalance);
                await _context.SaveChangesAsync();

                return anualBalance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AnualBalance> Get(int id,int userId)
        {
            try
            {
                var anualBalance = await _context.AnualBalances
                    .Include(b => b.Balances)
                    .ThenInclude(s => s.Spendings)
                    .Include(b => b.Balances)
                    .ThenInclude(i => i.Incomes)
                    .Include(b=> b.Balances)
                    .ThenInclude(m => m.Month)
                    .Where(b => b.Id == id && b.UserId == userId)
                    .FirstOrDefaultAsync();

                return anualBalance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<AnualBalance>> GetAll(int userId)
        {
            try
            {
                var anualBalances = await _context.AnualBalances
                    .Where(a => a.UserId == userId)
                    .OrderBy(a => a.Year)
                    .ToListAsync();

                return anualBalances;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AnualBalance> Update(AnualBalance entity)
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

        public bool Exist(int id)
        {
            return _context.AnualBalances.Any(e => e.Id == id);
        }

        //Validates the year is not already created.
        public async Task<bool> UniqueYear(int userId,int year)
        {
            try
            {
                var userAnualBalances = await _context.AnualBalances.Where(a => a.UserId == userId).ToListAsync();

                foreach (var item in userAnualBalances)
                {
                    if (item.Year == year)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddMonths(int anualBalanceId,int userId)
        {
            try
            {
                var months = await _balanceRepository.GetAllMonths();   

                foreach (Month month in months)
                {
                    Balance balance = new Balance();
                    balance.AnualBalanceId = anualBalanceId;
                    balance.Month = month;
                    
                    if(await _balanceRepository.Add(balance,userId) == null)
                    {
                        return false;
                    }
                }

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
                var user = await _context.Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
