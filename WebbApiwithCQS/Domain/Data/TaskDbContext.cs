using Microsoft.EntityFrameworkCore;
using WebbApiwithCQS.Domain.Entities;

namespace WebbApiwithCQS.Domain.Data;

public class TaskDbContext : DbContext
{
    public DbSet<Tache> Taches { get; set; }

    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tache>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Tache>()
            .Property(t => t.Titre)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Tache>()
            .Property(t => t.DateCreation)
            .HasColumnType("datetime2(7)")
            .HasDefaultValueSql("sysdatetime()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Tache>()
            .Property(t => t.Cloturee)
            .HasColumnType("bit")
            .HasDefaultValueSql("0");

        modelBuilder.Entity<Tache>()
            .HasData(
                new Tache()
                {
                    Id = 1,
                    Titre = "Design d'un site e-commerce avec l'outil figma",
                    DateCreation = new DateTime(2026, 05, 25),
                    Cloturee = true
                },
                new Tache()
                {
                    Id = 2,
                    Titre = "Développement d'un blog avec Angular 22 et ASP.NET Core 10",
                    DateCreation = new DateTime(2026, 08, 18)
                });
    }
}