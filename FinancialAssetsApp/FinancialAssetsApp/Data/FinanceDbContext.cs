using Microsoft.EntityFrameworkCore;
using FinancialAssetsApp.Models;


namespace FinancialAssetsApp.Data
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { } // Конструктор по умолчанию

        public DbSet<Stock> Stocks { get; set; }    // Для взаимодействия с БД, хранящая акции
        public DbSet<User> Users { get; set; } // Для взаимодействия с БД, хранящая юзеров

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.User)
                .WithMany(u => u.Stocks)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);   //При удалении пользовтеля удаление всех акций
        }
    }
}
