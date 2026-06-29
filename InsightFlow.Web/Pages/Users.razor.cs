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

    protected override async Task OnInitializedAsync()
    {
        try
        {
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