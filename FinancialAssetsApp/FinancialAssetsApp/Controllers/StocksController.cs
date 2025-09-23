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
        public async Task<IActionResult> Index()
        {
            var stocks = await _stocksService.GetAll();  // Перечисление всех данных из БД
            return View(stocks);
        }
        private void FillListCountries()
        {
            ViewBag.Countries = new List<SelectListItem>        // Создание списка для выбора страны компании
            {
                new SelectListItem {Value = "Russia", Text = "Россия"},
                new SelectListItem {Value="USA", Text = "США"}
            };
        }
        public IActionResult Create()
        {
            FillListCountries();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Stock stock)
        {
            if (ModelState.IsValid)
            {
                stock.SumStocks = stock.Price * stock.AmountStock;
                await _stocksService.Add(stock);
                return RedirectToAction("Index");
            }
            FillListCountries();
            return View(stock);
        }
        public IActionResult GetChart()
        {
            var data = _stocksService.GetChartDate();
            return Json(data);
        }

    }
}
