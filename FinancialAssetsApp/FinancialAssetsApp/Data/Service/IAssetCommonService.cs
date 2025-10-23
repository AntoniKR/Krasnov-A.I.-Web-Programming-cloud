using FinancialAssetsApp.Models;
using FinancialAssetsApp.Models.DTOs;

namespace FinancialAssetsApp.Data.Service
{
    public interface IAssetCommonService<T> //Общие действия для активов
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAssetsByID(int userId);
        Task<T?> GetAssetById(int userId);
        Task Add(T asset);
        Task Delete(int id);
    }
    public interface IStocksService : IAssetCommonService<Stock> // Построение графиков по тикерам рос. акций
    {
        Task<IEnumerable<ForChart>> GetChartTicker(int userId);
    }
    public interface IStocksUSDService : IAssetCommonService<StockUSD> // Построение графиков по тикерам иностранных акций
    {
        Task<IEnumerable<ForChart>> GetChartTicker(int userId);
    }
    public interface ICryptosService : IAssetCommonService<Crypto>    // График по тикерам крипты
    {
        Task<IEnumerable<ForChart>> GetChartTicker(int userId);
    }
    public interface IMetalsService : IAssetCommonService<Metal>    // График по металлам
    {
        Task<IEnumerable<ForChart>> GetChartTicker(int userId);
    }
    public interface ICurrenciesService : IAssetCommonService<Currency>
    {
        Task<IEnumerable<ForChart>> GetChartTicker(int userId);
    }
}
