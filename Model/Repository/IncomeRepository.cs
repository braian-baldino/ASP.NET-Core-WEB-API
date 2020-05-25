using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly DataContext _context;
        private readonly IBalanceRepository _balanceRepository;

        public IncomeRepository(DataContext context,IBalanceRepository balanceRepository)
        {
            _context = context;
            _balanceRepository = balanceRepository;
        }

        public async Task<Income> Add(Income income)
        {
            try
            {
                if (income == null)
                {
                    return null;
                }

                var category = await GetIncomeCategory(income.Category.Id);

                if(category == null)
                {
                    return null;
                }

                income.Category = category;

                await _context.AddAsync(income);
                await _context.SaveChangesAsync();

                return income;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Income> Delete(int id)
        {
            try
            {
                var income = await _context.Incomes.Where(b => b.Id == id).FirstOrDefaultAsync();

                if (income == null)
                    return null;

                _context.Remove(income);
                await _context.SaveChangesAsync();

                return income;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Income> Get(int id)
        {
            try
            {
                var income = await _context.Incomes.FirstOrDefaultAsync();

                return income;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Income>> GetAll()
        {
            try
            {
                var income = await _context.Incomes.OrderBy(i => i.Date).ToListAsync();

                return income;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Income> Update(Income entity)
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
            return _context.Incomes.Any(e => e.Id == id);
        }

        public async Task<bool> UpdateBalance(int balanceId)
        {
            return await _balanceRepository.Calculate(balanceId);     
        }

        public async Task<IncomeType> GetIncomeCategory(int incomeTypeId)
        {
            try
            {
                return await _context.IncomeCategories.Where(i => i.Id == incomeTypeId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
