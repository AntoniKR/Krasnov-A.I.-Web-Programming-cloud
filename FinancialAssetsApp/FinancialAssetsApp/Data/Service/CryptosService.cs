using BCrypt.Net;
using FinancialAssetsApp.Models;
using FinancialAssetsApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinancialAssetsApp.Data.Service
{
    public class CryptosService : ICryptosService
    {
        private readonly FinanceDbContext _context; // БД
        private readonly IAssetData _assetdata; // Для парсинга различных данных

        public CryptosService(FinanceDbContext context,IAssetData assetdata)  // Конструктор
        {
            _context = context;
            _assetdata = assetdata;
        }
        public async Task Add(Crypto crypto)  // Добавление крипты в БД
        {
            decimal rate = await _assetdata.GetCurrencyRate("USD"); ;   // Курс доллара

            var temp = crypto.Ticker.ToUpper();  //Перевод в верхний регистр
            crypto.Ticker = temp;

            crypto.SumCrypto = crypto.Price * crypto.AmountCrypto;
            crypto.SumCryptoToRuble = crypto.SumCrypto * rate;  // Перерасчет в рублях
            _context.Cryptos.Add(crypto);
            await _context.SaveChangesAsync();  // Асинхронно сохраняем изменения в БД
        }
        public async Task Delete(int id)    //Удаление акции
        {
            var crypto = await _context.Cryptos.FindAsync(id);
            if(crypto != null)
            {
                _context.Cryptos.Remove(crypto);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Crypto?> GetAssetById(int id)  //получение акции для удаления
        {
            return await _context.Cryptos.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Crypto>> GetAssetsByID(int userId)     //Перечисление всех акций пользователя
        {
            var crypto = await _context.Cryptos
                .Where(s => s.UserId == userId)
                .ToListAsync();
            return crypto;
        }
        
        public async Task<IEnumerable<Crypto>> GetAll()
        { 
            var crypto = await _context.Cryptos.ToListAsync();  // Перечисление всех данных из БД
            return crypto;
        }

        public async Task<IEnumerable<ForChart>> GetChartTicker(int userId) //График по акциям
        {
            var data = await _context.Cryptos
                .Where(s => s.UserId == userId)
                .GroupBy(e => e.Ticker)
                .Select(g => new ForChart
                {
                    Label = g.Key,
                    Total = g.Sum(e => e.SumCryptoToRuble) ?? 0m
                })
                .ToListAsync();
            return data;
        }
        public async Task FixOldCryptos()   // Для правок в БД
        {
            var cryptos = await _context.Cryptos.ToListAsync();
            decimal rate = await _assetdata.GetCurrencyRate("USD");

            foreach (var crypto in cryptos)
            {
                crypto.SumCrypto = crypto.Price * crypto.AmountCrypto;
                crypto.SumCryptoToRuble = crypto.SumCrypto * rate;
            }

            await _context.SaveChangesAsync();
        }

    }
}
