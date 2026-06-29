using System.Net.Http.Json;
using InsightFlow.Web.DTOs;

namespace InsightFlow.Web.Services;

public class DemandService
{
    private readonly HttpClient _httpClient;

    public DemandService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DemandDto>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<DemandDto>>("api/Demands")
            ?? new List<DemandDto>();
    }
}