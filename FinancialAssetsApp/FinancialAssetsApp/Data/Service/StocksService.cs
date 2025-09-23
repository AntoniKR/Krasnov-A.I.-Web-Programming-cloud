using FinancialAssetsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialAssetsApp.Data.Service
{
    public class StocksService : IStocksService
    {
        private readonly FinanceDbContext _context;

        public StocksService(FinanceDbContext context)
        {
            _context = context;
        }
        public async Task Add(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Stock>> GetAll()
        {
            var stocks = await _context.Stocks.ToListAsync();  // Перечисление всех данных из БД
            return stocks;
        }

        public IQueryable GetChartDate()
        {
            var data = _context.Stocks.GroupBy(e => e.Ticker).Select(g => new
            {
                Ticker = g.Key,
                Total = g.Sum(e => e.Price)
            });
            return data;

        }
    }
}
