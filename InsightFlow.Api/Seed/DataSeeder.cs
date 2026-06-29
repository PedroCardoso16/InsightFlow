using InsightFlow.Domain.Entities;
using InsightFlow.Domain.Enums;
using InsightFlow.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Api.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();

        await SeedUsersAsync(context);
        await SeedCategoriesAsync(context);
        await SeedDemandsAsync(context);
    }

    private static async Task SeedUsersAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync())
            return;

        var passwordHasher = new PasswordHasher<User>();

        var admin = new User
        {
            Id = Guid.NewGuid(),
            Name = "Administrador",
            Email = "admin@insightflow.com",
            Role = UserRole.Admin,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin@123");

        var analyst = new User
        {
            Id = Guid.NewGuid(),
            Name = "Analista de Dados",
            Email = "analista@insightflow.com",
            Role = UserRole.Analyst,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        analyst.PasswordHash = passwordHasher.HashPassword(analyst, "Analista@123");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Usuário Teste",
            Email = "usuario@insightflow.com",
            Role = UserRole.User,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "Usuario@123");

        await context.Users.AddRangeAsync(admin, analyst, user);
        await context.SaveChangesAsync();
    }

    private static async Task SeedCategoriesAsync(AppDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;

        var categories = new List<Category>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Business Intelligence",
                Description = "Demandas relacionadas a BI, indicadores e análise de dados.",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Automação",
                Description = "Demandas relacionadas à automação de processos.",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Integração de Sistemas",
                Description = "Demandas relacionadas à integração entre aplicações e APIs.",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Governança de Dados",
                Description = "Demandas relacionadas à organização, qualidade e governança das informações.",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }

    private static async Task SeedDemandsAsync(AppDbContext context)
    {
        if (await context.Demands.AnyAsync())
            return;

        var admin = await context.Users.FirstOrDefaultAsync(user => user.Email == "admin@insightflow.com");
        var analyst = await context.Users.FirstOrDefaultAsync(user => user.Email == "analista@insightflow.com");
        var commonUser = await context.Users.FirstOrDefaultAsync(user => user.Email == "usuario@insightflow.com");

        var biCategory = await context.Categories.FirstOrDefaultAsync(category => category.Name == "Business Intelligence");
        var automationCategory = await context.Categories.FirstOrDefaultAsync(category => category.Name == "Automação");
        var integrationCategory = await context.Categories.FirstOrDefaultAsync(category => category.Name == "Integração de Sistemas");

        if (admin is null || analyst is null || commonUser is null)
            return;

        if (biCategory is null || automationCategory is null || integrationCategory is null)
            return;

        var demands = new List<Demand>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Criar dashboard de indicadores",
                Description = "Desenvolver dashboard para acompanhamento de demandas por status, prioridade e categoria.",
                Priority = DemandPriority.High,
                Status = DemandStatus.Open,
                CategoryId = biCategory.Id,
                CreatedByUserId = commonUser.Id,
                AssignedToUserId = analyst.Id,
                Notes = "Demanda criada para demonstrar análise de dados no sistema.",
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Automatizar processo interno",
                Description = "Mapear e automatizar um processo manual para aumentar eficiência operacional.",
                Priority = DemandPriority.Medium,
                Status = DemandStatus.InProgress,
                CategoryId = automationCategory.Id,
                CreatedByUserId = admin.Id,
                AssignedToUserId = analyst.Id,
                Notes = "Exemplo de demanda relacionada à automação.",
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Integrar API com sistema externo",
                Description = "Criar integração entre o InsightFlow e um sistema externo via API REST.",
                Priority = DemandPriority.Critical,
                Status = DemandStatus.Completed,
                CategoryId = integrationCategory.Id,
                CreatedByUserId = analyst.Id,
                AssignedToUserId = admin.Id,
                Notes = "Demanda concluída para demonstrar cálculo de tempo médio de resolução.",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow.AddDays(-2),
                CompletedAt = DateTime.UtcNow.AddDays(-2)
            }
        };

        await context.Demands.AddRangeAsync(demands);
        await context.SaveChangesAsync();
    }
}