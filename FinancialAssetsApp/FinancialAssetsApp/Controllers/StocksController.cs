using FinancialAssetsApp.Data;
using FinancialAssetsApp.Data.Service;
using FinancialAssetsApp.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create()
        {
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
            return View(stock);
        }
        public IActionResult GetChart()
        {
            var data = _stocksService.GetChartDate();
            return Json(data);
        }

    }
}
