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
    public interface IStocksService : IAssetCommonService<Stock> // Построение графиков по тикерам и странам
    {
        Task<IEnumerable<ForChart>> GetChartTicker(int userId);
        Task<IEnumerable<ForChart>> GetChartCountry(int userId);
    }
    public interface ICryptosService : IAssetCommonService<Crypto>    // График по тикерам
    {
        Task<IEnumerable<ForChart>> GetChartTicker(int userId);
    }
    public interface IMetalsService : IAssetCommonService<Metal>    // График по тикерам
    {
        Task<IEnumerable<ForChart>> GetChartTicker(int userId);
    }
}
