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
    public class SpendingRepository : ISpendingRepository
    {
        private readonly DataContext _context;
        private readonly IBalanceRepository _balanceRepository;

        public SpendingRepository(DataContext context,IBalanceRepository balanceRepository)
        {
            _context = context;
            _balanceRepository = balanceRepository;
        }

        public async Task<Spending> Add(Spending spending)
        {
            try
            {
                if (spending == null)
                {
                    return null;
                }

                var category = await GetSpendingCategory(spending.Category.Id);

                if (category == null)
                {
                    return null;
                }

                spending.Category = category;

                await _context.AddAsync(spending);
                await _context.SaveChangesAsync();

                return spending;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Spending> Delete(int id)
        {
            try
            {
                var spending = await _context.Spendings.Where(b => b.Id == id).FirstOrDefaultAsync();

                if (spending == null)
                    return null;

                _context.Remove(spending);
                await _context.SaveChangesAsync();

                return spending;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Spending> Get(int id)
        {
            try
            {
                var spending = await _context.Spendings.FirstOrDefaultAsync();

                return spending;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Spending>> GetAll()
        {
            try
            {
                var spending = await _context.Spendings.OrderBy(i => i.Date).ToListAsync();

                return spending;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Spending> Update(Spending entity)
        {
            try
            {
                if (entity == null)
                    return null;

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }

        public bool Exist(int id)
        {
            return _context.Spendings.Any(e => e.Id == id);
        }

        public async Task<bool> UpdateBalance(int balanceId)
        {
            return await _balanceRepository.Calculate(balanceId);
        }

        public async Task<SpendingType> GetSpendingCategory(int spendingTypeId)
        {
            try
            {
                return await _context.SpendingCategories.Where(i => i.Id == spendingTypeId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
