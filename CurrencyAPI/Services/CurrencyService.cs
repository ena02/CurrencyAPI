using CurrencyAPI.DbContexts;
using CurrencyAPI.Model;
using CurrencyAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Xml.Linq;

namespace CurrencyAPI.Services
{
    public class CurrencyService : ICurrencyRepository
    {
        private readonly CurrencyDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public CurrencyService(CurrencyDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> SaveCurrencyData(DateTime date)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = $"https://nationalbank.kz/rss/get_rates.cfm?fdate={date:dd.MM.yyyy}";

            try
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var xml = XDocument.Parse(content);

                    var currencies = xml.Root.Elements("item").Select(item => new Currency
                    {
                        Title = item.Element("fullname")?.Value,
                        Code = item.Element("title")?.Value,
                        Value = decimal.Parse(item.Element("description")?.Value),
                        ADate = date
                    }).ToList();

                    _context.R_CURRENCY.AddRange(currencies);
                    await _context.SaveChangesAsync();

                    return currencies.Count;
                }
                else
                {
                    // В случае неудачного запроса к API возвращаем -1 или выбрасываем исключение
                    throw new Exception($"Failed to fetch data from the National Bank API. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Логируем исключение или обрабатываем ошибку
                throw new Exception($"Error saving currency data: {ex.Message}", ex);
            }
        }

        public async Task<List<Currency>> GetCurrencyData(DateTime date, string code = null)
        {
            IQueryable<Currency> query = _context.R_CURRENCY.Where(c => c.ADate == date);

            if (code != null)
            {
                query = query.Where(c => c.Code == code);
            }

            return await query.ToListAsync();
        }
    }
}
