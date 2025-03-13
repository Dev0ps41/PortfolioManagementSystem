using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

public class StockMarketService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public StockMarketService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["StockMarketApi:ApiKey"];
        _baseUrl = configuration["StockMarketApi:BaseUrl"];
    }

    public async Task<decimal?> GetStockPrice(string ticker)
    {
        var url = $"{_baseUrl}?function=GLOBAL_QUOTE&symbol={ticker}&apikey={_apiKey}";
        var response = await _httpClient.GetStringAsync(url);

        var data = JObject.Parse(response);
        var priceString = data["Global Quote"]?["05. price"]?.ToString();

        if (decimal.TryParse(priceString, out decimal price))
        {
            return price;
        }

        return null;
    }
}
