using FinancialAssetsApp.Models;

namespace FinancialAssetsApp.Data.Service
{
    public interface IStocksService
    {
        Task<IEnumerable<Stock>> GetAll();
        Task Add(Stock stock);
        IQueryable GetChartDate();
    }
}
