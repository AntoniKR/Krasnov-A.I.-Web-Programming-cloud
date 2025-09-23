

using System.ComponentModel.DataAnnotations;

namespace FinancialAssetsApp.Models
{
    public class Stock
    {
        public int Id { get; set; }                 // Идентификация
        [Required]
        public string Ticker { get; set; }          // Тикер акции 
        public string NameCompany { get; set; }     // Название компании
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена меньше 0!")] //Ограничение на ввод
        public decimal Price { get; set; }           // Цена акции
        public DateTime DateAddStock { get; set; } = DateTime.UtcNow;    // Время обновления
    }
}
