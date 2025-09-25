using FinancialAssetsApp.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinancialAssetsApp.Data.Service
{
    public class StocksService : IStocksService
    {
        private readonly FinanceDbContext _context;
        private readonly IAssetData _assetdata;

        public StocksService(FinanceDbContext context,IAssetData assetdata)  // Конструктор
        {
            _context = context;
            _assetdata = assetdata;
        }
        public async Task Add(Stock stock)  // Добавление акции в БД
        {
            decimal rate = 1;   // Если акции российские, то сумма остается той же
            if (stock.Country == "США")
                rate = await _assetdata.GetRateAsset("USD");

            stock.SumStocks = stock.Price * stock.AmountStock;
            stock.SumStocksToRuble = stock.SumStocks * rate;  // Перерасчет в рублях
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();  // Асинхронно сохраняем изменения в БД
        }
        public async Task Delete(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if(stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Stock> GetStockById(int id) => await _context.Stocks.FindAsync(id);
        
        public async Task<IEnumerable<Stock>> GetAll()
        { 
            var stocks = await _context.Stocks.ToListAsync();  // Перечисление всех данных из БД
            return stocks;
        }

        public IQueryable GetChartTicker()
        {
            var data = _context.Stocks.GroupBy(e => e.Ticker).Select(g => new
            {
                Ticker = g.Key,
                Total = g.Sum(e => e.SumStocksToRuble)
            });
            return data;
        }
        public IQueryable GetChartCountry()
        {
            var data = _context.Stocks.GroupBy(e => e.Country).Select(g => new
            {
                Country = g.Key,
                Total = g.Sum(e => e.SumStocksToRuble)
            });
            return data;
        }












        public async Task FixOldStocks()
        {
            var stocks = await _context.Stocks
                .Where(s => s.SumStocksToRuble == null && s.AmountStock > 0)
                .ToListAsync();

            foreach (var stock in stocks)
            {
                decimal rate = 1;   // Если акции российские, то сумма остается той же
                if (stock.Country == "США")
                    rate = await _assetdata.GetRateAsset("USD");
                stock.SumStocks = stock.Price * stock.AmountStock;
                stock.SumStocksToRuble = stock.SumStocks * rate;
            }

            await _context.SaveChangesAsync();
        }
    }
}
