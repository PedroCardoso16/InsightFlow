using InsightFlow.Web.DTOs;
using InsightFlow.Web.Services;
using Microsoft.AspNetCore.Components;

namespace InsightFlow.Web.Pages;

public partial class Categories : ComponentBase
{
    [Inject]
    protected CategoryService CategoryService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    protected List<CategoryDto> categories = new();

    protected bool isLoading = true;

    protected string errorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            categories = await CategoryService.GetAllAsync();
        }
        catch
        {
            errorMessage = "Não foi possível carregar as categorias. Verifique sua autenticação ou permissão.";
        }
        finally
        {
            isLoading = false;
        }
    }

    protected void GoToDashboard()
    {
        NavigationManager.NavigateTo("/");
    }
}