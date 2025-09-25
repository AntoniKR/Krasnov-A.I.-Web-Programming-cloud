using FinancialAssetsApp.Data;
using FinancialAssetsApp.Data.Service;
using FinancialAssetsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinancialAssetsApp.Controllers
{
    public class StocksController : Controller
    {
        private readonly IStocksService _stocksService;
        public StocksController(IStocksService stocksService)
        {
            _stocksService = stocksService;
        }
        
        public async Task<IActionResult> Index()    // Список всех акций
        {
            var stocks = await _stocksService.GetAll();  // Перечисление всех данных из БД
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
            if (ModelState.IsValid)
            {
                await _stocksService.Add(stock);
                return RedirectToAction("Index");
            }
            FillListCountries();
            return View(stock);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _stocksService.GetStockById(id);
            if (stock == null)
                return NotFound();
            return View(stock);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _stocksService.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult GetChartT()
        {
            var data = _stocksService.GetChartTicker();
            return Json(data);
        }
        public IActionResult GetChartC()
        {
            var data = _stocksService.GetChartCountry();
            return Json(data);
        }



















        public async Task<IActionResult> FixSums()
        {
            await _stocksService.FixOldStocks();
            return RedirectToAction("Index");
        }
    }
}
