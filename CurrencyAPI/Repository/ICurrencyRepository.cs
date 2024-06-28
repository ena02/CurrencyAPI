using CurrencyAPI.Model;

namespace CurrencyAPI.Repository
{
    public interface ICurrencyRepository
    {
        Task<int> SaveCurrencyData(DateTime date);
        Task<List<Currency>> GetCurrencyData(DateTime date, string code = null);
    }
}
