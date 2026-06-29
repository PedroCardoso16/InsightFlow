using InsightFlow.Web.DTOs;
using InsightFlow.Web.Services;
using Microsoft.AspNetCore.Components;

namespace InsightFlow.Web.Pages;

public partial class EditUser : ComponentBase
{
    [Parameter]
    public Guid Id { get; set; }

    [Inject]
    protected UserService UserService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    protected UpdateUserDto updateUser = new();

    protected bool isLoading = true;
    protected bool isSubmitting = false;

    protected string errorMessage = string.Empty;
    protected string successMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var user = await UserService.GetByIdAsync(Id);

            if (user is null)
            {
                errorMessage = "Usuário não encontrado.";
                return;
            }

            updateUser = new UpdateUserDto
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }
        catch
        {
            errorMessage = "Não foi possível carregar o usuário.";
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

        if (string.IsNullOrWhiteSpace(updateUser.Name))
        {
            errorMessage = "O nome do usuário é obrigatório.";
            return;
        }

        if (string.IsNullOrWhiteSpace(updateUser.Email))
        {
            errorMessage = "O e-mail do usuário é obrigatório.";
            return;
        }

        isSubmitting = true;

        var result = await UserService.UpdateAsync(Id, updateUser);

        if (result is null)
        {
            errorMessage = "Não foi possível atualizar o usuário.";
            isSubmitting = false;
            return;
        }

        successMessage = "Usuário atualizado com sucesso.";

        await Task.Delay(800);

        NavigationManager.NavigateTo("/usuarios");
    }

    protected void GoBack()
    {
        NavigationManager.NavigateTo("/usuarios");
    }
}