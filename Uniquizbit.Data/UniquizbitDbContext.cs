namespace Uniquizbit.Data
{
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore;
  using Models;

  public class UniquizbitDbContext : IdentityDbContext<User>
  {
    public DbSet<Answer> Answers { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Score> Scores { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    public DbSet<Quiz> Quizzes { get; set; }

    public DbSet<QuizGroup> QuizGroups { get; set; }

    public DbSet<QuizProgress> QuizProgresses { get; set; }

    public DbSet<QuizSubscription> QuizSubscriptions { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<GroupSubscription> GroupSubscriptions { get; set; }

    public DbSet<QuizzesUsers> QuizzesUsers { get; set; }

    public DbSet<GroupsTags> GroupsTags { get; set; }

    public DbSet<QuizzesTags> QuizzesTags { get; set; }

    public DbSet<ProgressAnswer> ProgressAnswers { get; set; }

    public UniquizbitDbContext(DbContextOptions options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Tag>()
          .HasIndex(t => t.Name)
          .IsUnique();

      modelBuilder.Entity<QuizGroup>()
        .HasMany(qg => qg.Quizzes)
        .WithOne(q => q.Group)
        .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<QuizGroup>()
        .HasIndex(qg => qg.Name)
        .IsUnique();

      modelBuilder.Entity<Quiz>()
        .HasIndex(qg => qg.Name)
        .IsUnique();

      modelBuilder.Entity<Quiz>()
        .HasMany(q => q.Questions)
        .WithOne(a => a.Quiz)
        .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<UsersGroups>()
        .HasKey(ug => new { ug.UserId, ug.QuizGroupId });

      modelBuilder.Entity<Rating>()
        .HasKey(r => new { r.UserId, r.QuizId });

      modelBuilder.Entity<Score>()
        .HasKey(s => new { s.UserId, s.QuizId });

      modelBuilder.Entity<GroupSubscription>()
        .HasKey(gs => new { gs.UserId, gs.QuizGroupId });

      modelBuilder.Entity<QuizSubscription>()
        .HasKey(qs => new { qs.UserId, qs.QuizId });

      modelBuilder.Entity<QuizzesUsers>()
        .HasKey(qu => new { qu.QuizId, qu.UserId });

      modelBuilder.Entity<QuizzesTags>()
        .HasKey(qu => new { qu.QuizId, qu.TagId });

      modelBuilder.Entity<GroupsTags>()
        .HasKey(qu => new { qu.GroupId, qu.TagId });

      modelBuilder.Entity<QuizProgress>()
        .HasKey(qp => new {qp.QuizId, qp.UserId});

      modelBuilder.Entity<QuizProgress>()
        .HasMany(qp => qp.GivenAnswers)
        .WithOne(ga => ga.QuizProgress)
        .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<User>()
        .HasMany(u => u.OwnQuizzes)
        .WithOne(q => q.Creator);


      modelBuilder.Entity<Question>()
        .HasMany(q => q.Answers)
        .WithOne(a => a.Question)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
