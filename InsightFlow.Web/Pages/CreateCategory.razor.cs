using InsightFlow.Web.DTOs;
using InsightFlow.Web.Services;
using Microsoft.AspNetCore.Components;

namespace InsightFlow.Web.Pages;

public partial class CreateCategory : ComponentBase
{
    [Inject]
    protected CategoryService CategoryService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    protected CreateCategoryDto createCategory = new();

    protected bool isSubmitting = false;

    protected string errorMessage = string.Empty;
    protected string successMessage = string.Empty;

    protected async Task HandleSubmit()
    {
        errorMessage = string.Empty;
        successMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(createCategory.Name))
        {
            errorMessage = "O nome da categoria é obrigatório.";
            return;
        }

        isSubmitting = true;

        var result = await CategoryService.CreateAsync(createCategory);

        if (result is null)
        {
            errorMessage = "Não foi possível cadastrar a categoria.";
            isSubmitting = false;
            return;
        }

        successMessage = "Categoria cadastrada com sucesso.";

        await Task.Delay(800);

        NavigationManager.NavigateTo("/categorias");
    }

    protected void GoBack()
    {
        NavigationManager.NavigateTo("/categorias");
    }
}