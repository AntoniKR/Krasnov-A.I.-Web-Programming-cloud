using FinancialAssetsApp.Data;
using FinancialAssetsApp.Data.Service;
using FinancialAssetsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FinancialAssetsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);  // Поддержка кодировок(для API металлов)
            builder.Services.AddDbContext<FinanceDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));  // Подключение к POstgres

            // Подключение MVC
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<IAssetData, AssetData>();
            builder.Services.AddScoped<IStocksService, StocksService>();
            builder.Services.AddScoped<IStocksUSDService, StocksUSDservice>();
            builder.Services.AddScoped<IAuthService, AuthService>();            
            builder.Services.AddScoped<ICryptosService, CryptosService>();
            builder.Services.AddScoped<HomeService>();
            builder.Services.AddScoped<IMetalsService, MetalsService>();
            builder.Services.AddScoped<ICurrenciesService, CurrenciesService>();
            builder.Services.AddScoped<IPlatformStartupService, PlatformStartupsService>();


            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>  // Если сессия была без активности 30 минут, то выход
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            var app = builder.Build();

            
                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.Use(async (context, next) =>    //Автологин, закомментить, если вход с страницы логина
            {
                // Если сессия ещё не установлена
                if (!context.Session.Keys.Contains("User"))
                {
                    var authService = context.RequestServices.GetRequiredService<IAuthService>();
                    string adminUsername = "admin";
                    string adminPassword = "123";

                    if (await authService.ValidateUser(adminUsername, adminPassword))
                    {
                        var user = await authService.GetUserByName(adminUsername);
                        context.Session.SetString("User", user.Username);
                        context.Session.SetInt32("UserId", user.Id);
                    }
                }

                await next.Invoke();
            });

            app.UseRouting();
            app.UseAuthorization(); // Авторизация юзера
            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Account}/{action=Login}/{id?}");    // Раскомментить, если вход с логина
                pattern: "{controller=Home}/{action=Index}/{id?}");         // Закомменить
            app.Run();
        }
    }
}
