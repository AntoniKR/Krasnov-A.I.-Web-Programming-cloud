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
                .SumAsync(e => e.SumStocksToRuble) ?? 0;
            var totalCrypto = await _context.Cryptos
                .Where(s => s.UserId == userId)
                .SumAsync(e => e.SumCryptoToRuble) ?? 0;
            return new List<ForChart>
            {
                new ForChart{Label = "Акции", Total = totalStocks},
                new ForChart{Label = "Криптовалюта", Total = totalCrypto}
            };
        }
        public async Task<decimal> GetRate()
        {
            return await _assetdata.GetRateAsset("USD");
        }
    }
}
