namespace FinancialAssetsApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; //Хэшированный пароль

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>(); // Связь с акциями
        public ICollection<Crypto> Cryptos { get; set; } = new List<Crypto>(); // Связь с криптой
        public ICollection<Metal> Metals { get; set; } = new List<Metal>(); // Связь с металлами
        public ICollection<StockUSD> StocksUSD { get; set; } = new List<StockUSD>(); // Связь с акциями американскими

    }
}
