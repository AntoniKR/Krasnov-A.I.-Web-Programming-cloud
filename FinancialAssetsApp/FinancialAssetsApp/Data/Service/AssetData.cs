using Microsoft.IdentityModel.Protocols;
using System.Net.Http;
using System.Text.Json;

namespace FinancialAssetsApp.Data.Service
{
    public class AssetData : IAssetData
    {
        private readonly HttpClient _httpClient;
        public AssetData(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<decimal> GetRateAsset(string code)    // Получение курса валюты
        {
            var dataAsset = await _httpClient.GetStringAsync("https://www.cbr-xml-daily.ru/daily_json.js");
            var doc = JsonDocument.Parse(dataAsset);

            var currency = doc.RootElement.GetProperty("Valute");

            if (currency.TryGetProperty(code, out var rateInfo))
                return rateInfo.GetProperty("Value").GetDecimal();

            throw new Exception($"Валюта {code} не найдена");
        }
    }
}
