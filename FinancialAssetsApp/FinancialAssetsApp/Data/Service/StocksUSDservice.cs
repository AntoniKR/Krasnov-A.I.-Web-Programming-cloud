 using FinancialAssetsApp.Models;
using FinancialAssetsApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinancialAssetsApp.Data.Service
{
    public class StocksUSDservice : IStocksUSDService
    {
        private readonly FinanceDbContext _context; // БД
        private readonly IAssetData _assetdata; // Для парсинга различных курсов

        public StocksUSDservice(FinanceDbContext context,IAssetData assetdata)  // Конструктор
        {
            _context = context;
            _assetdata = assetdata;
        }
        public async Task Add(StockUSD stock)  // Добавление акции в БД
        {
            decimal rate = await _assetdata.GetCurrencyRate("USD");   // Для расчета акций в рублях
            var temp = stock.Ticker.ToUpper();  //Перевод в верхний регистр
            stock.Ticker = temp;
            stock.SumStocks = stock.Price * stock.AmountStock;
            stock.SumStocksToRuble = stock.SumStocks * rate;
            _context.StocksUSD.Add(stock);
            await _context.SaveChangesAsync();  // Асинхронно сохраняем изменения в БД
        }
        public async Task Delete(int id)    //Удаление акции
        {
            var stock = await _context.StocksUSD.FindAsync(id);
            if(stock != null)
            {
                _context.StocksUSD.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<StockUSD?> GetAssetById(int id)  //получение акции для удаления
        {
            return await _context.StocksUSD.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<StockUSD>> GetAssetsByID(int userId)     //Перечисление всех акций пользователя
        {
            var stocks = await _context.StocksUSD
                .Where(s => s.UserId == userId)
                .ToListAsync();
            return stocks;
        }
        
        public async Task<IEnumerable<StockUSD>> GetAll()
        { 
            var stocks = await _context.StocksUSD.ToListAsync();  // Перечисление всех данных из БД
            return stocks;
        }

        public async Task<IEnumerable<ForChart>> GetChartTicker(int userId) //График по акциям
        {
            var data = await _context.StocksUSD
                .Where(s => s.UserId == userId)
                .GroupBy(e => e.Ticker)
                .Select(g => new ForChart
                {
                    Label = g.Key,
                    Total = g.Sum(e => e.SumStocks) ?? 0m
                })
                .ToListAsync();
            return data;
        }
        /*public async Task<IEnumerable<ForChart>> GetChartCountry(int userId)    //График по странам
        {
            var data = await _context.Stocks
                .Where(s => s.UserId == userId)
                .GroupBy(e => e.Country)
                .Select(g => new ForChart
                {
                    Label = g.Key ?? "не указано",
                    Total = g.Sum(e => e.SumStocksToRuble).GetValueOrDefault()
                })
                .ToListAsync();
            return data;
        }*/












        /*public async Task FixOldStocks()
        {
            var stocks = await _context.Stocks
                .Where(s => s.SumStocksToRuble == null && s.AmountStock > 0)
                .ToListAsync();

            foreach (var stock in stocks)
            {
                decimal rate = 1;   // Если акции российские, то сумма остается той же
                if (stock.Country == "США")
                    rate = await _assetData.GetCurrencyRate("USD");
                stock.SumStocks = stock.Price * stock.AmountStock;
                stock.SumStocksToRuble = stock.SumStocks * rate;
            }

            await _context.SaveChangesAsync();
        }*/
    }
}
