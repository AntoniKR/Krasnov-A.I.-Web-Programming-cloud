using FinancialAssetsApp.Models;
using FinancialAssetsApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAssetsApp.Data.Service
{
    public interface ICryptosService
    {
        Task<IEnumerable<Crypto>> GetAll();
        Task<IEnumerable<Crypto>> GetCryptosByID(int userId);
        Task<Crypto?> GetCryptoById(int userId);

        Task Add(Crypto crypto);
        Task Delete(int id);
        Task<IEnumerable<ForChart>> GetChartTicker(int userId);
        Task FixOldCryptos();

    }
}
