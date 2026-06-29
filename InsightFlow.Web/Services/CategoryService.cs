using System.Net.Http.Json;
using InsightFlow.Web.DTOs;

namespace InsightFlow.Web.Services;

public class CategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CategoryDto>>("api/Categories")
            ?? new List<CategoryDto>();
    }
}