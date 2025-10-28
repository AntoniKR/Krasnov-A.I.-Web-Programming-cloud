using FinancialAssetsApp.Models;
using FinancialAssetsApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FinancialAssetsApp.Data.Service
{
    public class HomeService
    {
        private readonly FinanceDbContext _context; // БД
        private readonly IAssetData _assetdata; // Для парсинга различных курсов

        public HomeService(FinanceDbContext context, IAssetData assetdata)  // Конструктор
        {
            _context = context;
            _assetdata = assetdata;
        }
        public async Task<IEnumerable<ForChart>> GetAssetsSumm (int userId)
        {
            var totalStocks = await _context.Stocks
                .Where(s => s.UserId == userId)
                .SumAsync(e => e.SumStocks) ?? 0;
            var totalStocksUSD = await _context.StocksUSD
                .Where(s => s.UserId == userId)
                .SumAsync(e => e.SumStocksToRuble);
            var totalCrypto = await _context.Cryptos
                .Where(s => s.UserId == userId)
                .SumAsync(e => e.SumCryptoToRuble);
            var totalMetals = await _context.Metals
                .Where(s => s.UserId == userId)
                .SumAsync(e => e.SumMetals) ?? 0;
            var totalCurrencies = await _context.Currencies
                .Where(s => s.UserId == userId)
                .SumAsync(e => e.SumCurrencyToRuble);
            return new List<ForChart>
            {
                new ForChart{Label = "Акции ₽", Total = totalStocks},
                new ForChart{Label = "Акции $", Total = totalStocksUSD},
                new ForChart{Label = "Криптовалюта", Total = totalCrypto},
                new ForChart{Label = "Металлы", Total = totalMetals},
                new ForChart{Label = "Валюта", Total = totalCurrencies},
            };
        }
        public async Task<decimal> GetRate()
        {
            return await _assetdata.GetCurrencyRate("USD");
        }
    }
}
