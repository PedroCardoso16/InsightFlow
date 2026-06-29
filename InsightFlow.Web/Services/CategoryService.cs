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

    public async Task<CategoryDto?> CreateAsync(CreateCategoryDto createCategoryDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Categories", createCategoryDto);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CategoryDto>();
    }
}