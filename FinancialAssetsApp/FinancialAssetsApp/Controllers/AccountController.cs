using FinancialAssetsApp.Data;
using Microsoft.AspNetCore.Mvc;
using FinancialAssetsApp.Models;
using BCrypt.Net;


namespace FinancialAssetsApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly FinanceDbContext _authService;
        public AccountController(FinanceDbContext authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = _authService.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                HttpContext.Session.SetString("User", user.Username);
                return RedirectToAction("Index", "Stocks");

            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
