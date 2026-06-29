using InsightFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Demand> Demands { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureUsers(modelBuilder);
        ConfigureCategories(modelBuilder);
        ConfigureDemands(modelBuilder);
    }

    private static void ConfigureUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(user => user.Id);

            entity.Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(user => user.Email)
                .IsUnique();

            entity.Property(user => user.PasswordHash)
                .IsRequired();

            entity.Property(user => user.Role)
                .IsRequired();

            entity.Property(user => user.IsActive)
                .IsRequired();

            entity.Property(user => user.CreatedAt)
                .IsRequired();
        });
    }

    private static void ConfigureCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");

            entity.HasKey(category => category.Id);

            entity.Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(category => category.Description)
                .HasMaxLength(300);

            entity.Property(category => category.IsActive)
                .IsRequired();

            entity.Property(category => category.CreatedAt)
                .IsRequired();
        });
    }

    private static void ConfigureDemands(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Demand>(entity =>
        {
            entity.ToTable("Demands");

            entity.HasKey(demand => demand.Id);

            entity.Property(demand => demand.Title)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(demand => demand.Description)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(demand => demand.Priority)
                .IsRequired();

            entity.Property(demand => demand.Status)
                .IsRequired();

            entity.Property(demand => demand.Notes)
                .HasMaxLength(1000);

            entity.Property(demand => demand.CreatedAt)
                .IsRequired();

            entity.HasOne(demand => demand.Category)
                .WithMany(category => category.Demands)
                .HasForeignKey(demand => demand.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(demand => demand.CreatedByUser)
                .WithMany(user => user.CreatedDemands)
                .HasForeignKey(demand => demand.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(demand => demand.AssignedToUser)
                .WithMany(user => user.AssignedDemands)
                .HasForeignKey(demand => demand.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}