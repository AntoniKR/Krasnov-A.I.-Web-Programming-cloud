using FinancialAssetsApp.Data;
using Microsoft.AspNetCore.Mvc;
using FinancialAssetsApp.Models;
using BCrypt.Net;


namespace FinancialAssetsApp.Controllers
{
    public class AccountController : Controller //Авторизация пользователя
    {
        private readonly FinanceDbContext _authService;
        public AccountController(FinanceDbContext authService)  //Доступ к БД через конструктор
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["NoLayout"] = true;    //Убираем плашку layout
            return View();
        }
        //Возвращает страницу логина

        [HttpPost]
        public IActionResult Login(LoginModel model)    //Проверка для входа
        {
            if (!ModelState.IsValid) return View(model);
            var user = _authService.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                // После успешного логина
                HttpContext.Session.SetString("User", user.Username);
                
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Index", "Stocks");

            }

            //если не совпадает 
            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel model)  // Добавление нового пользователя
        {
            if (!ModelState.IsValid) return View(model);
            if(_authService.Users.Any(u => u.Username == model.Username))
            {   //Проверка на существующего пользователя
                ModelState.AddModelError("", "Такой пользователь существует");
                return View(model);
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User
            {
                Username = model.Username,
                PasswordHash = passwordHash
            };
            _authService.Users.Add(user);
            _authService.SaveChanges();

            HttpContext.Session.SetString("User", user.Username);
            return RedirectToAction("Index", "Stocks");
        }
        public IActionResult Logout()   //Выход из сессии
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ForgotPassword()   //Замена пароля
        {
            return View();
        }
        //Возвращает страницу логина

        [HttpPost]
        public IActionResult ForgotPassword(string username, string newPassword)    //Проверка для входа
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                ModelState.AddModelError("", "Введите логин и новый пароль");
                return View();
            }

            var user = _authService.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return View();
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);    // новый пароль
            _authService.SaveChanges();

            ViewBag.Message = "Пароль изменен!";
            return RedirectToAction("Login");
        }

    }
}
