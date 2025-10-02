namespace FinancialAssetsApp.Data.Service
{
    public interface IAssetData     //Интерфейс для курса различных активов
    {
        Task<decimal> GetCurrencyRate(string code);    //курс валют
        Task<decimal> GetMetalRate(string code);    // курс металлов
        Task<decimal> RUgetStockPrice(string ticker);    // курс металлов

    }
}
