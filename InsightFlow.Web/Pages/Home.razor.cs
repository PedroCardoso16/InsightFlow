using InsightFlow.Web.DTOs;
using InsightFlow.Web.Services;
using Microsoft.AspNetCore.Components;

namespace InsightFlow.Web.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    protected DashboardService DashboardService { get; set; } = default!;

    [Inject]
    protected AuthService AuthService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    protected DashboardSummaryDto? summary;
    protected DashboardResolutionTimeDto? resolutionTime;

    protected List<DashboardByStatusDto> byStatus = new();
    protected List<DashboardPriorityPercentageDto> priorityPercentages = new();
    protected List<DashboardTopCategoryDto> topCategories = new();
    protected List<DashboardMonthlyDto> monthlyEvolution = new();

    protected bool isLoading = true;
    protected string errorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            summary = await DashboardService.GetSummaryAsync();
            byStatus = await DashboardService.GetByStatusAsync();
            priorityPercentages = await DashboardService.GetPriorityPercentagesAsync();
            topCategories = await DashboardService.GetTopCategoriesAsync();
            monthlyEvolution = await DashboardService.GetMonthlyEvolutionAsync();
            resolutionTime = await DashboardService.GetAverageResolutionTimeAsync();
        }
        catch
        {
            errorMessage = "Não foi possível carregar o dashboard. Faça login novamente.";
        }
        finally
        {
            isLoading = false;
        }
    }

    protected async Task Logout()
    {
        await AuthService.LogoutAsync();
        NavigationManager.NavigateTo("/login");
    }

    protected void GoToLogin()
    {
        NavigationManager.NavigateTo("/login");
    }

    protected static string TranslateStatus(string status)
    {
        return status switch
        {
            "Open" => "Aberta",
            "InProgress" => "Em andamento",
            "Completed" => "Concluída",
            "Canceled" => "Cancelada",
            _ => status
        };
    }

    protected static string TranslatePriority(string priority)
    {
        return priority switch
        {
            "Low" => "Baixa",
            "Medium" => "Média",
            "High" => "Alta",
            "Critical" => "Crítica",
            _ => priority
        };
    }

    protected void GoToDemands()
    {
        NavigationManager.NavigateTo("/demandas");
    }


    protected void GoToCategories()
    {
        NavigationManager.NavigateTo("/categorias");
    }

    protected void GoToUsers()
    {
        NavigationManager.NavigateTo("/usuarios");
    }
}