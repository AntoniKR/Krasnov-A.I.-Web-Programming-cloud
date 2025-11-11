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
    public class StartupsController : Controller
    {
        private readonly IStartupService _startupService;
        private readonly IAssetData _assetdata; // Для парсинга различных курсов
        private int CurrentUserId => HttpContext.Session.GetInt32("UserId") ?? 0;
        public StartupsController(IStartupService startupService, IAssetData assetdata)
        {
            _startupService = startupService;
            _assetdata = assetdata;
        }

        public async Task<IActionResult> Index()    // Список всех стартапов
        {
            var startup = await _startupService.GetAssetsByID(CurrentUserId);  // Перечисление всех данных из БД
            return View("IndexStartup", startup);
        }
        private void FillListPlatforms()    // Метод для списка платформ
        {
            ViewBag.PlatformStartup = new List<SelectListItem>        // Создание списка для выбора платформы
            {
                new SelectListItem {Value = "BrainBox", Text = "BrainBox"},
                new SelectListItem {Value = "Zorko", Text = "Zorko"},
                new SelectListItem {Value = "Поток", Text = "Поток"},
                new SelectListItem {Value = "Bizmall", Text = "Bizmall"},
                new SelectListItem {Value = "Zapusk", Text = "Zapusk"},
                new SelectListItem {Value = "Finmuster", Text = "Finmuster"},
                new SelectListItem {Value = "Rounds", Text = "Rounds"}
            };
        }
        public IActionResult Create()   // Страница добавления акции
        {

            FillListPlatforms();
            return View("CreateStartup");
        }
        [HttpPost]
        public async Task<IActionResult> Create(Startup startup)
        {
            startup.UserId = CurrentUserId;  //Привязка к текущему пользователю
            startup.PlatformStartupId = await _startupService.GetPlatformId(startup.NamePlatform);
            if (!ModelState.IsValid)
            {
                FillListPlatforms();
                return View("CreateStartup", startup);
            }
            await _startupService.Add(startup);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            
            var startup = await _startupService.GetAssetById(id);
            if (startup == null || startup.UserId != CurrentUserId)    //Проверка на платформу
                return NotFound();
            return View("DeleteStartup", startup);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var startup = await _startupService.GetAssetById(id);
            if (startup == null || startup.UserId != CurrentUserId)    //Проверка на акции текущего пользователя
                return NotFound();
            await _startupService.Delete(id);
            return RedirectToAction();
        }
        public async Task<IActionResult> GetChartT()
        {
            var data = await _startupService.GetChartTicker(CurrentUserId);
            return Json(data);
        }
        public async Task<IActionResult> PriceMetal(string nameMetal)   //Получение цены на металлы
        {
            var price = await _assetdata.GetMetalPrice(nameMetal);
            return Json(price);
        }


        /*public async Task<IActionResult> FixSums()
        {
            await _stocksService.FixOldStocks();
            return RedirectToAction("IndexStocks");
        }*/
    }
}
