using System.Net.Http.Json;
using InsightFlow.Web.DTOs;

namespace InsightFlow.Web.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly TokenService _tokenService;

    public AuthService(HttpClient httpClient, TokenService tokenService)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginDto);

        if (!response.IsSuccessStatusCode)
            return null;

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

        if (authResponse is not null && !string.IsNullOrWhiteSpace(authResponse.Token))
        {
            await _tokenService.SetTokenAsync(authResponse.Token);
        }

        return authResponse;
    }

    public async Task LogoutAsync()
    {
        await _tokenService.RemoveTokenAsync();
    }
}