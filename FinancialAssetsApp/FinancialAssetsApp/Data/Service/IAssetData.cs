namespace FinancialAssetsApp.Data.Service
{
    public interface IAssetData     //Интерфейс для курса различных активов
    {
        Task<decimal> GetRateAsset(string code); 
    }
}
