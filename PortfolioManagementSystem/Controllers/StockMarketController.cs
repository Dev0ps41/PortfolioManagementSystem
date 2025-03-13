using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class StockMarketController : ControllerBase
{
    private readonly StockMarketService _stockService;

    public StockMarketController(StockMarketService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet("GetStockPrice/{ticker}")]
    public async Task<ActionResult<decimal?>> GetStockPrice(string ticker)
    {
        var price = await _stockService.GetStockPrice(ticker);
        if (price == null) return NotFound("Stock price not available.");
        return Ok(price);
    }
}
