using InsightFlow.Web.DTOs;
using InsightFlow.Web.Services;
using Microsoft.AspNetCore.Components;

namespace InsightFlow.Web.Pages;

public partial class CreateUser : ComponentBase
{
    [Inject]
    protected UserService UserService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    protected CreateUserDto createUser = new();

    protected bool isSubmitting = false;

    protected string errorMessage = string.Empty;
    protected string successMessage = string.Empty;

    protected async Task HandleSubmit()
    {
        errorMessage = string.Empty;
        successMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(createUser.Name))
        {
            errorMessage = "O nome do usuário é obrigatório.";
            return;
        }

        if (string.IsNullOrWhiteSpace(createUser.Email))
        {
            errorMessage = "O e-mail do usuário é obrigatório.";
            return;
        }

        if (string.IsNullOrWhiteSpace(createUser.Password))
        {
            errorMessage = "A senha do usuário é obrigatória.";
            return;
        }

        if (createUser.Password.Length < 6)
        {
            errorMessage = "A senha deve ter pelo menos 6 caracteres.";
            return;
        }

        isSubmitting = true;

        var result = await UserService.CreateAsync(createUser);

        if (result is null)
        {
            errorMessage = "Não foi possível cadastrar o usuário. Verifique se o e-mail já está em uso ou se você possui permissão.";
            isSubmitting = false;
            return;
        }

        successMessage = "Usuário cadastrado com sucesso.";

        await Task.Delay(800);

        NavigationManager.NavigateTo("/usuarios");
    }

    protected void GoBack()
    {
        NavigationManager.NavigateTo("/usuarios");
    }
}