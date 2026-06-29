using InsightFlow.Web.DTOs;
using InsightFlow.Web.Services;
using Microsoft.AspNetCore.Components;

namespace InsightFlow.Web.Pages;

public partial class EditCategory : ComponentBase
{
    [Parameter]
    public Guid Id { get; set; }

    [Inject]
    protected CategoryService CategoryService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    protected UpdateCategoryDto updateCategory = new();

    protected bool isLoading = true;
    protected bool isSubmitting = false;

    protected string errorMessage = string.Empty;
    protected string successMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var category = await CategoryService.GetByIdAsync(Id);

            if (category is null)
            {
                errorMessage = "Categoria não encontrada.";
                return;
            }

            updateCategory = new UpdateCategoryDto
            {
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };
        }
        catch
        {
            errorMessage = "Não foi possível carregar a categoria.";
        }
        finally
        {
            isLoading = false;
        }
    }

    protected async Task HandleSubmit()
    {
        errorMessage = string.Empty;
        successMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(updateCategory.Name))
        {
            errorMessage = "O nome da categoria é obrigatório.";
            return;
        }

        isSubmitting = true;

        var result = await CategoryService.UpdateAsync(Id, updateCategory);

        if (result is null)
        {
            errorMessage = "Não foi possível atualizar a categoria.";
            isSubmitting = false;
            return;
        }

        successMessage = "Categoria atualizada com sucesso.";

        await Task.Delay(800);

        NavigationManager.NavigateTo("/categorias");
    }

    protected void GoBack()
    {
        NavigationManager.NavigateTo("/categorias");
    }
}