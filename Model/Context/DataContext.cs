using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Entities;

namespace Model.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<AnualBalance> AnualBalances { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Spending> Spendings { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<IncomeType> IncomeCategories { get; set; }
        public DbSet<SpendingType> SpendingCategories { get; set; }
        
    }
}
