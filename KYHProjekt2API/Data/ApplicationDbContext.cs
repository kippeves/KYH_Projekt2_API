using Microsoft.EntityFrameworkCore;

namespace KYHProjekt2API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<TimeRegistration> TimeRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimeRegistration>()
            .HasOne(e => e.Customer)
            .WithMany(e => e.TimeRegistrations)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TimeRegistration>()
            .HasOne(e => e.Project)
            .WithMany(e => e.TimeRegistrations)
            .OnDelete(DeleteBehavior.Restrict);
    }
}