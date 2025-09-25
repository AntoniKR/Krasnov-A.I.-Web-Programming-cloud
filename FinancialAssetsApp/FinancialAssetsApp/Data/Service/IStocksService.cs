using FinancialAssetsApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAssetsApp.Data.Service
{
    public interface IStocksService
    {
        Task<IEnumerable<Stock>> GetAll();
        Task Add(Stock stock);
        Task<Stock>GetStockById(int id);
        Task Delete(int id);
        IQueryable GetChartTicker();
        IQueryable GetChartCountry();

        Task FixOldStocks();

    }
}
