using System.Text.Json;

namespace cShard_Blazor;

public class CategoryService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions options;
    public CategoryService( HttpClient httpClient, JsonSerializerOptions optionsJson)
    {
        client = httpClient;
        options = optionsJson;
    }

    public async Task<List<Product>?> Get()
    {
        var response = await client.GetAsync("/v1/products");
        return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
    }
}