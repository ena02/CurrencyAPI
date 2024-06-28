using CurrencyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyAPI.Controllers
{

    [Route("currency/")]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyService _currencyService;

        public CurrencyController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("save/{date}")]
        public async Task<IActionResult> SaveCurrencyData(DateTime date)
        {
            try
            {
                var count = await _currencyService.SaveCurrencyData(date);
                return Ok(new { count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Failed to save currency data: {ex.Message}" });
            }
        }

        
        [HttpGet("{date}/{code?}")]
        public async Task<IActionResult> GetCurrencyData(DateTime date, string code = null)
        {
            try
            {
                var currencies = await _currencyService.GetCurrencyData(date, code);
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Failed to retrieve currency data: {ex.Message}" });
            }
        }

    } 
}
