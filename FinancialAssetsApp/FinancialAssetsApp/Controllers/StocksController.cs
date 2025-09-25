using FinancialAssetsApp.Data;
using FinancialAssetsApp.Data.Service;
using FinancialAssetsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace FinancialAssetsApp.Controllers
{
    public class StocksController : Controller
    {
        private readonly IStocksService _stocksService;
        private int CurrentUserId => HttpContext.Session.GetInt32("UserId") ?? 0;
        public StocksController(IStocksService stocksService)
        {
            _stocksService = stocksService;
        }

        public async Task<IActionResult> Index()    // Список всех акций
        {
            var stocks = await _stocksService.GetStocksByID(CurrentUserId);  // Перечисление всех данных из БД
            return View(stocks);
        }
        private void FillListCountries()    // Метод для списка стран 
        {
            ViewBag.Countries = new List<SelectListItem>        // Создание списка для выбора страны компании
            {
                new SelectListItem {Value = "Россия", Text = "Россия"},
                new SelectListItem {Value="США", Text = "США"}
            };
        }
        public IActionResult Create()   // Страница добавления акции
        {
            FillListCountries();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Stock stock)
        {
            stock.UserId = CurrentUserId;  //Привязка к текущему пользователю

            if (!ModelState.IsValid)
            {
                FillListCountries();
                return View(stock);
            }

            await _stocksService.Add(stock);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _stocksService.GetStockById(id);
            if (stock == null || stock.UserId != CurrentUserId)    //Проверка на акции текущего пользователя
                return NotFound();
            return View(stock);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _stocksService.GetStockById(id);
            if (stock == null || stock.UserId != CurrentUserId)    //Првоерка на акции текущего пользователя
                return NotFound();
            await _stocksService.Delete(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetChartT()
        {
            var data = await _stocksService.GetChartTicker(CurrentUserId);
            return Json(data);
        }
        public async Task<IActionResult> GetChartC()
        {
            var data = await _stocksService.GetChartCountry(CurrentUserId);
            return Json(data);
        }






        public async Task<IActionResult> FixSums()
        {
            await _stocksService.FixOldStocks();
            return RedirectToAction("Index");
        }
    }
}
