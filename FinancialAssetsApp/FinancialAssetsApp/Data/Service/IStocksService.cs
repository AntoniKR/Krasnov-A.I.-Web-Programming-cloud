using FinancialAssetsApp.Models;
using FinancialAssetsApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAssetsApp.Data.Service
{
    public interface IStocksService
    {
        Task<IEnumerable<Stock>> GetAll();
        Task<IEnumerable<Stock>> GetStocksByID(int userId);
        Task<Stock?> GetStockById(int userId);

        Task Add(Stock stock);
        Task Delete(int id);
        Task<IEnumerable<ForChart>> GetChartTicker(int userId);
        Task<IEnumerable<ForChart>> GetChartCountry(int userId);

        Task FixOldStocks();

    }
}
