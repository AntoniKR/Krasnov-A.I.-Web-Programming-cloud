using FinancialAssetsApp.Data;
using FinancialAssetsApp.Data.Service;
using FinancialAssetsApp.Models;
using FinancialAssetsApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Net.Http;

namespace FinancialAssetsApp.Controllers
{
    public class CryptoController : Controller
    {
        public readonly ICryptosService _cryptosService;
        private readonly HttpClient _httpClient;
        private int CurrentUserId => HttpContext.Session.GetInt32("UserId") ?? 0;
        public CryptoController(ICryptosService cryptosService, HttpClient httpClient) 
        {
            _cryptosService = cryptosService;
            _httpClient = httpClient;
        }
        public async Task<IActionResult> GetTickersCrypto(string symbols)   //Получение списка крипта с Bybit
        {
            var urlTickers = "https://api.bybit.com/v5/market/tickers?category=spot";
            var response = await _httpClient.GetStringAsync(urlTickers);
            var json = JsonDocument.Parse(response);

            var tickers = json.RootElement
                .GetProperty("result")
                .GetProperty("list")
                .EnumerateArray()
                .Select(x => x.GetProperty("symbol").GetString())
                .Where(s => string.IsNullOrEmpty(symbols) || s.StartsWith(symbols.ToUpper()))
                .Take(20)
                .ToList();
            return Json(tickers);
        }

        
        public async Task<IActionResult> GetPriceCrypto (string symbols)    //Получение текущей цены крипты
        {
            var urlTickers = "https://api.bybit.com/v5/market/tickers?category=spot";
            var response = await _httpClient.GetStringAsync(urlTickers);
            var json = JsonDocument.Parse(response);

            var ticker = json.RootElement
                .GetProperty("result")
                .GetProperty("list")
                .EnumerateArray()
                .FirstOrDefault(x => x.GetProperty("symbol").GetString() == symbols);

            if (ticker.ValueKind == JsonValueKind.Undefined)
                return Json("not found(");
            var price = ticker.GetProperty("lastPrice").GetString();
            return Json(price);

        }

        public async Task<IActionResult> IndexCrypto()    // Список всей крипты
        {
            var cryptos = await _cryptosService.GetAssetsByID(CurrentUserId);
            //await FixCrypto();    // Для правок в БД
            return View(cryptos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Crypto crypto)
        {
            crypto.UserId = CurrentUserId;
            if (!ModelState.IsValid)
            {
                return View(crypto);
            }

            await _cryptosService.Add(crypto);
            return RedirectToAction("IndexCrypto");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var crypto = await _cryptosService.GetAssetById(id);
            if (crypto == null || crypto.UserId != CurrentUserId)    //Проверка на акции текущего пользователя
                return NotFound();
            return View("DeleteCrypto",crypto);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crypto = await _cryptosService.GetAssetById(id);
            if (crypto == null || crypto.UserId != CurrentUserId)    //Проверка на акции текущего пользователя
                return NotFound();
            await _cryptosService.Delete(id);
            return RedirectToAction("IndexCrypto");
        }
        public async Task<IActionResult> GetChartTicker()
        {
            var data = await _cryptosService.GetChartTicker(CurrentUserId);
            return Json(data);
        }
        /*public async Task<IActionResult> FixCrypto()
        {
            await _cryptosService.FixOldCryptos();
            return RedirectToAction("IndexStocks");
        }*/
    }
}
