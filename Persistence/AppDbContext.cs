using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence
{ 
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		// DbSets
		public DbSet<Answer> Answers { get; set; }

		public DbSet<Tag> Tags { get; set; }

		public DbSet<Score> Scores { get; set; }

		public DbSet<Rating> Ratings { get; set; }

		public DbSet<Quiz> Quizes { get; set; }

		public DbSet<QuizGroup> QuizGroups { get; set; }

		public DbSet<QuizProgress> QuizProgresses { get; set; }

		public DbSet<QuizSubscription> QuizSubscribtions { get; set; }

		public DbSet<Question> Questions { get; set; }

		public DbSet<Notification> Notifications { get; set; }

		public DbSet<GroupSubscribtion> GroupSubscribtions { get; set; }

		public AppDbContext(DbContextOptions options) 
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
					.HasIndex(qg => qg.Name)
					.IsUnique();

				modelBuilder.Entity<UsersGroups>()
					.HasKey(ug => new {ug.UserId, ug.QuizGroupId});

				modelBuilder.Entity<Rating>()
					.HasKey(r => new {r.UserId, r.QuizId});

				modelBuilder.Entity<GroupSubscribtion>()
					.HasKey(gs => new {gs.UserId, gs.QuizGroupId});

				modelBuilder.Entity<QuizSubscription>()
					.HasKey(qs => new {qs.UserId, qs.QuizId});
		}
	}
}
