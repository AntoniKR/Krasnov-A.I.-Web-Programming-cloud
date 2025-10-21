using Microsoft.IdentityModel.Protocols;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Linq;
using System.Globalization;

namespace FinancialAssetsApp.Data.Service
{
    public class AssetData : IAssetData
    {
        private readonly HttpClient _httpClient;
        public AssetData(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<decimal> GetCurrencyRate(string code)    // Получение курса валюты
        {
            var dataAsset = await _httpClient.GetStringAsync("https://www.cbr-xml-daily.ru/daily_json.js");
            var doc = JsonDocument.Parse(dataAsset);

            var currency = doc.RootElement.GetProperty("Valute");

            if (currency.TryGetProperty(code, out var rateInfo))
                return rateInfo.GetProperty("Value").GetDecimal();

            throw new Exception($"Валюта {code} не найдена");
        }
        public async Task<decimal> GetMetalRate(string code)    // Получение курса металла
        {
            DateTime dateTime = DateTime.UtcNow.ToLocalTime();
            dateTime = dateTime.AddDays(-1);
            string day = dateTime.ToString("dd");
            Console.WriteLine(day);
            string month = dateTime.ToString("MM");
            string year = dateTime.ToString("yyyy");
            try
            {
                var url = $"https://www.cbr.ru/scripts/xml_metall.asp?date_req1={day}/{month}/{year}&date_req2={day}/{month}/{year}";
                Console.WriteLine($"[DEBUG] URL: {url}");

                var dataAsset = await _httpClient.GetStringAsync(url);

                var doc = XDocument.Parse(dataAsset);
                var record = doc.Descendants("Record")
                    .FirstOrDefault(r => r.Attribute("Code")?.Value == code);
                if (record == null)
                    throw new Exception($"Нет записи с Code={code}");

                var sell = record.Element("Sell")!.Value.Replace(',', '.');
                return decimal.Parse(sell, NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении металла: {ex.Message}");
                throw;
            }
            /*var url = $"https://www.cbr.ru/scripts/xml_metall.asp?date_req1={day}/{month}/{year}&date_req2={day}/{month}/{year}";
            
            var dataAsset = await _httpClient.GetStringAsync(url);

            var dataAsset = await _httpClient.GetStringAsync($"https://www.cbr.ru/scripts/xml_metall.asp?date_req1={day}/{month}/{year}&date_req2={day}/{month}/{year}");
            
            var doc = XDocument.Parse(dataAsset);
            var record = doc.Descendants("Record")
                .FirstOrDefault(r => r.Attribute("Code")?.Value == code);

            var sell = record.Element("Sell")!.Value.Replace(',', '.');
            return decimal.Parse(sell, NumberStyles.Any, CultureInfo.InvariantCulture);


            throw new Exception($"Металл {code} не найден");*/
        }
        public async Task<decimal> RUgetStockPrice(string ticker)
        {
            try
            {
                var urlTicker = $"https://iss.moex.com/iss/engines/stock/markets/shares/securities/{ticker}.json";
                var response = await _httpClient.GetStringAsync(urlTicker);
                using var doc = JsonDocument.Parse(response);

                var rows = doc.RootElement
                    .GetProperty("marketdata")
                    .GetProperty("data");

                if (rows.GetArrayLength() == 0)
                    return 0;
                var row = rows[0];

                decimal price = 0;

                if (row[4].ValueKind != JsonValueKind.Null)
                    price = row[4].GetDecimal();
                else if (row[2].ValueKind != JsonValueKind.Null)
                    price = row[2].GetDecimal();

                return price;
            }
            catch
            {
                return 0;
            }
            






            /*var marketdata = doc.RootElement.GetProperty("marketdata");
            var secdata = doc.RootElement.GetProperty("securities");

            var mdCols = marketdata.GetProperty("columns").EnumerateArray()
                .Select((col, index) => new { Name = col.GetString(), Index = index })
                .ToDictionary(x => x.Name, x => x.Index);
            var secCols = secdata.GetProperty("columns").EnumerateArray()
                .Select((col, index) => new { Name = col.GetString(), Index = index })
                .ToDictionary(x => x.Name, x => x.Index);

            int prevPrice = secCols["PREVPRICE"];
            int boardId = secCols["BOARDID"];

            int lastIndex = mdCols.ContainsKey("LAST") ? mdCols["LAST"] : -1;
            int mdBoard = mdCols["BOARDID"];

            foreach ( var row in marketdata.GetProperty("data").EnumerateArray())
            {
                var boardID = row[mdBoard].GetString();
                if (boardID == "TQBR" && lastIndex >= 0 && row[lastIndex].ValueKind == JsonValueKind.Number)
                    return row[prevPrice].GetDecimal();
            }

            throw new Exception($"Не удалось найти цену закрытия для {ticker}");*/
        }
    }
}
