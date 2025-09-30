using FinancialAssetsApp.Data;
using FinancialAssetsApp.Data.Service;
using FinancialAssetsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace FinancialAssetsApp.Controllers
{
    public class CryptoController : Controller
    {
        public readonly ICryptosService _cryptosService;
        private int CurrentUserId => HttpContext.Session.GetInt32("UserId") ?? 0;
        public CryptoController(ICryptosService cryptosService) 
        {
            _cryptosService = cryptosService;
        }
        public async Task<IActionResult> IndexCrypto()    // Список всей крипты
        {
            var cryptos = await _cryptosService.GetCryptosByID(CurrentUserId);
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
            var crypto = await _cryptosService.GetCryptoById(id);
            if (crypto == null || crypto.UserId != CurrentUserId)    //Проверка на акции текущего пользователя
                return NotFound();
            return View("DeleteCrypto",crypto);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crypto = await _cryptosService.GetCryptoById(id);
            if (crypto == null || crypto.UserId != CurrentUserId)    //Проверка на акции текущего пользователя
                return NotFound();
            await _cryptosService.Delete(id);
            return RedirectToAction("IndexCrypto");
        }


        public async Task<IActionResult> GetChartCrypto()
        {
            var data = await _cryptosService.GetChartTicker(CurrentUserId);
            return Json(data);
        }
        public async Task<IActionResult> FixCrypto()
        {
            await _cryptosService.FixOldCryptos();
            return RedirectToAction("IndexStocks");
        }

    }
}
