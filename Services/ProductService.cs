using System.Net.Http.Json;
using System.Runtime.Intrinsics;
using System.Text.Json;

namespace cShard_Blazor;

public class ProductService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions options;
    public ProductService( HttpClient httpClient, JsonSerializerOptions optionsJson)
    {
        client = httpClient;
        options = optionsJson;
    }

    public async Task<List<Product>?> Get()
    {
        var response = await client.GetAsync("/v1/products");
        return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
    }

    public async Task Add(Product product)
    {
        var response = await client.PostAsync("/v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

    }

    public async Task Delete(int productId)
    {
        var response = await client.PostAsync("/v1/products", JsonContent.Create(productId));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

    }
}