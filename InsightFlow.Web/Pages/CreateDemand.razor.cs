using InsightFlow.Web.DTOs;
using InsightFlow.Web.Services;
using Microsoft.AspNetCore.Components;

namespace InsightFlow.Web.Pages;

public partial class CreateDemand : ComponentBase
{
    [Inject]
    protected DemandService DemandService { get; set; } = default!;

    [Inject]
    protected CategoryService CategoryService { get; set; } = default!;

    [Inject]
    protected UserService UserService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    protected CreateDemandDto createDemand = new();

    protected List<CategoryDto> categories = new();
    protected List<UserDto> users = new();

    protected bool isLoading = true;
    protected bool isSubmitting = false;

    protected string errorMessage = string.Empty;
    protected string successMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            categories = await CategoryService.GetAllAsync();
            users = await UserService.GetAllAsync();
        }
        catch
        {
            errorMessage = "Não foi possível carregar categorias e usuários. Verifique suas permissões.";
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

        if (string.IsNullOrWhiteSpace(createDemand.Title))
        {
            errorMessage = "O título é obrigatório.";
            return;
        }

        if (string.IsNullOrWhiteSpace(createDemand.Description))
        {
            errorMessage = "A descrição é obrigatória.";
            return;
        }

        if (createDemand.CategoryId == Guid.Empty)
        {
            errorMessage = "Selecione uma categoria.";
            return;
        }

        if (createDemand.CreatedByUserId == Guid.Empty)
        {
            errorMessage = "Selecione o usuário solicitante.";
            return;
        }

        isSubmitting = true;

        var result = await DemandService.CreateAsync(createDemand);

        if (result is null)
        {
            errorMessage = "Não foi possível cadastrar a demanda.";
            isSubmitting = false;
            return;
        }

        successMessage = "Demanda cadastrada com sucesso.";

        await Task.Delay(800);

        NavigationManager.NavigateTo("/demandas");
    }

    protected void HandleAssignedUserChange(ChangeEventArgs e)
    {
        var value = e.Value?.ToString();

        if (string.IsNullOrWhiteSpace(value))
        {
            createDemand.AssignedToUserId = null;
            return;
        }

        if (Guid.TryParse(value, out var userId))
        {
            createDemand.AssignedToUserId = userId;
        }
    }

    protected void GoBack()
    {
        NavigationManager.NavigateTo("/demandas");
    }
}