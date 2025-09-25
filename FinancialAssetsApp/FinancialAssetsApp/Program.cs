using FinancialAssetsApp.Data;
using FinancialAssetsApp.Data.Service;
using Microsoft.EntityFrameworkCore;

namespace FinancialAssetsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<FinanceDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));  // Подключение к POstgres

            // Подключение MVC
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IStocksService, StocksService>();

            builder.Services.AddHttpClient<IAssetData, AssetData>();

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
            app.UseRouting();
            app.UseAuthorization(); // Авторизация юзера
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Stocks}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
