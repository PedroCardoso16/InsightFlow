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
    protected string successMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadCategories();
    }

    protected async Task LoadCategories()
    {
        try
        {
            isLoading = true;
            errorMessage = string.Empty;

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

    protected void GoToCreateCategory()
    {
        NavigationManager.NavigateTo("/categorias/nova");
    }

    protected void GoToEditCategory(Guid id)
    {
        NavigationManager.NavigateTo($"/categorias/editar/{id}");
    }

    protected async Task InactivateCategory(Guid id)
    {
        errorMessage = string.Empty;
        successMessage = string.Empty;

        var result = await CategoryService.DeleteAsync(id);

        if (!result)
        {
            errorMessage = "Não foi possível inativar a categoria.";
            return;
        }

        successMessage = "Categoria inativada com sucesso.";

        await LoadCategories();
    }
}