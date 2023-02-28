using System.Net.Http.Json;

public class ProductService : IProductService<ProductDto>
{
    private readonly HttpClient _httpClient;
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetAllProducts()
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<ProductDto>>>("/products");
        if (!result.Success)
        {
            Console.WriteLine(result.Message);
        }
        Products = result.Data;
    }

    public async Task GetProductByCategory(string category)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<ProductDto>>>($"/categoryproduct/{category}");
        if (!result.Success)
        {
            Console.WriteLine(result.Message);
        }
        Products = result.Data;
    }
    public async Task<ServiceResponse<ProductDto>> GetProductByName(string name)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductDto>>($"/productname/{name}");
        if (!result.Success)
        {
            Console.WriteLine(result.Message);
        }
        return result;
    }
    public async Task<ServiceResponse<ProductDto>> GetProductByNumber(string number)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductDto>>($"/productnumber/{number}");
        if (!result.Success)
        {
            Console.WriteLine(result.Message);
        }
        return result;
    }

    public async Task GetProductBySearchText(string text)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<ProductDto>>>($"/searchproduct/{text}");
        if (!result.Success)
        {
            Console.WriteLine(result.Message);
        }
        Products = result.Data;
    }
}

