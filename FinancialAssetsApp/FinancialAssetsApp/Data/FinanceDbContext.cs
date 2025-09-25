using Microsoft.EntityFrameworkCore;
using FinancialAssetsApp.Models;


namespace FinancialAssetsApp.Data
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { } // Конструктор по умолчанию

        public DbSet<Stock> Stocks { get; set; }    // Для взаимодействия с БД, хранящая акции
        public DbSet<User> Users { get; set; } // Для взаимодействия с БД, хранящая юзеров
    }
}
