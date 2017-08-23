using Microsoft.EntityFrameworkCore;
using App.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace App.Persistence
{
  public class AppDbContext : IdentityDbContext<ApplicationUser>
  {
    // DbSets
    public DbSet<Quiz> Quizes { get; set; }
    
    public DbSet<Answer> Answers { get; set; }

    public DbSet<QuizGroup> QuizGroups { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Score> Scores { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    public DbSet<QuizProgress> QuizProgresses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) 
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ApplicationUser>()
          .ToTable("Users");
        
        modelBuilder.Entity<IdentityRole>()
          .ToTable("Roles");

        modelBuilder.Entity<Tag>()
          .HasIndex(t => t.Name)
          .IsUnique();

        modelBuilder.Entity<QuizGroup>()
          .HasIndex(qg => qg.Name)
          .IsUnique();
    }
  }
}