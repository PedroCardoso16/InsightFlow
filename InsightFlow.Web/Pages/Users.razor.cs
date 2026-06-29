using InsightFlow.Web.DTOs;
using InsightFlow.Web.Services;
using Microsoft.AspNetCore.Components;

namespace InsightFlow.Web.Pages;

public partial class Users : ComponentBase
{
    [Inject]
    protected UserService UserService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    protected List<UserDto> users = new();

    protected bool isLoading = true;

    protected string errorMessage = string.Empty;
    protected string successMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadUsers();
    }

    protected async Task LoadUsers()
    {
        try
        {
            isLoading = true;
            errorMessage = string.Empty;

            users = await UserService.GetAllAsync();
        }
        catch
        {
            errorMessage = "Não foi possível carregar os usuários. Verifique se você está logado como Admin.";
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

    protected void GoToCreateUser()
    {
        NavigationManager.NavigateTo("/usuarios/novo");
    }

    protected void GoToEditUser(Guid id)
    {
        NavigationManager.NavigateTo($"/usuarios/editar/{id}");
    }

    protected async Task InactivateUser(Guid id)
    {
        errorMessage = string.Empty;
        successMessage = string.Empty;

        var result = await UserService.DeleteAsync(id);

        if (!result)
        {
            errorMessage = "Não foi possível inativar o usuário.";
            return;
        }

        successMessage = "Usuário inativado com sucesso.";

        await LoadUsers();
    }

    protected static string TranslateRole(int role)
    {
        return role switch
        {
            1 => "Admin",
            2 => "Analista",
            3 => "Usuário",
            _ => "Desconhecido"
        };
    }
}