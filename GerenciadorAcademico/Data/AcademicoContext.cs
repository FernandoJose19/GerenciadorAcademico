using GerenciadorAcademico.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorAcademico.Data;

public class AcademicoContext : DbContext
{
    public AcademicoContext(DbContextOptions<AcademicoContext> options): base(options)
    {
    }

    public DbSet<Professor> Professores => Set<Professor>();

    public DbSet<Projeto> Projetos => Set<Projeto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Professor>(e =>
        {
            e.ToTable("Professores");

            e.HasKey(p => p.Id);

            e.Property(p => p.Nome) .IsRequired().HasMaxLength(100);
            e.Property(p => p.Email).IsRequired();
        });

        modelBuilder.Entity<Projeto>(e =>
        {
            e.ToTable("Projetos");

            e.HasKey(p => p.Id);

            e.Property(p => p.Titulo).IsRequired().HasMaxLength(150);

            e.Property(p => p.Descricao).IsRequired(false).HasMaxLength(1000);

            e.Property(p => p.CargaHoraria).IsRequired();

            e.Property(p => p.Area).IsRequired(false).HasMaxLength(80);

            e.HasOne(p => p.Professor)
            .WithMany(p => p.Projetos)
            .HasForeignKey(p => p.ProfessorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        });
    }
}