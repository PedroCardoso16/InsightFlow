using System.Net.Http.Json;
using InsightFlow.Web.DTOs;

namespace InsightFlow.Web.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<UserDto>>("api/Users")
            ?? new List<UserDto>();
    }
}