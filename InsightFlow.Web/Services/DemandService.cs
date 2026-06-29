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

    public async Task<DemandDto?> CreateAsync(CreateDemandDto createDemandDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Demands", createDemandDto);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<DemandDto>();
    }

    public async Task<DemandDto?> UpdateStatusAsync(Guid id, int status)
    {
        var updateStatusDto = new UpdateDemandStatusDto
        {
            Status = status
        };

        var response = await _httpClient.PatchAsJsonAsync($"api/Demands/{id}/status", updateStatusDto);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<DemandDto>();
    }
}