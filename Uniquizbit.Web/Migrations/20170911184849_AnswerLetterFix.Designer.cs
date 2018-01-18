﻿// <auto-generated />
using Uniquizbit.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Uniquizbit.Migrations
{
    [DbContext(typeof(UniquizbitDbContext))]
    [Migration("20170911184849_AnswerLetterFix")]
    partial class AnswerLetterFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("App.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsRight");

                    b.Property<string>("Letter")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<int>("QuestionId");

                    b.Property<int>("QuizId");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuizId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("App.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("App.Models.GroupsTags", b =>
                {
                    b.Property<int>("GroupId");

                    b.Property<int>("TagId");

                    b.HasKey("GroupId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("GroupsTags");
                });

            modelBuilder.Entity("App.Models.GroupSubscription", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizGroupId");

                    b.HasKey("UserId", "QuizGroupId");

                    b.ToTable("GroupSubscriptions");
                });

            modelBuilder.Entity("App.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("IssuerId");

                    b.Property<string>("Message");

                    b.Property<int?>("QuizGroupId");

                    b.Property<int?>("QuizId");

                    b.Property<bool>("Seen");

                    b.HasKey("Id");

                    b.HasIndex("IssuerId");

                    b.HasIndex("QuizGroupId");

                    b.HasIndex("QuizId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("App.Models.ProgressesAnswers", b =>
                {
                    b.Property<int>("ProgressId");

                    b.Property<int>("AnswerId");

                    b.Property<int?>("ProgressQuestionId");

                    b.Property<int?>("ProgressQuizId");

                    b.Property<string>("ProgressUserId");

                    b.HasKey("ProgressId", "AnswerId");

                    b.HasIndex("AnswerId");

                    b.HasIndex("ProgressQuizId", "ProgressQuestionId", "ProgressUserId");

                    b.ToTable("ProgressesAnswers");
                });

            modelBuilder.Entity("App.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsMultiselect");

                    b.Property<int>("MaxAnswers");

                    b.Property<int>("QuizId");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("App.Models.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CreatorId");

                    b.Property<DateTime>("Ends");

                    b.Property<int?>("GroupId");

                    b.Property<bool>("Locked");

                    b.Property<bool>("Once");

                    b.Property<string>("Password");

                    b.Property<DateTime>("Starts");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("GroupId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("App.Models.QuizGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.ToTable("QuizGroups");
                });

            modelBuilder.Entity("App.Models.QuizProgress", b =>
                {
                    b.Property<int>("QuizId");

                    b.Property<int>("QuestionId");

                    b.Property<string>("UserId");

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("QuizId", "QuestionId", "UserId");

                    b.ToTable("QuizProgresses");
                });

            modelBuilder.Entity("App.Models.QuizSubscription", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizId");

                    b.HasKey("UserId", "QuizId");

                    b.ToTable("QuizSubscriptions");
                });

            modelBuilder.Entity("App.Models.QuizzesTags", b =>
                {
                    b.Property<int>("QuizId");

                    b.Property<int>("TagId");

                    b.HasKey("QuizId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("QuizzesTags");
                });

            modelBuilder.Entity("App.Models.QuizzesUsers", b =>
                {
                    b.Property<int>("QuizId");

                    b.Property<string>("UserId");

                    b.HasKey("QuizId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("QuizzesUsers");
                });

            modelBuilder.Entity("App.Models.Rating", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizId");

                    b.Property<double>("Value");

                    b.HasKey("UserId", "QuizId");

                    b.HasIndex("QuizId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("App.Models.Score", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizId");

                    b.Property<DateTime>("ScoredAt");

                    b.Property<double>("Value");

                    b.HasKey("UserId", "QuizId");

                    b.HasIndex("QuizId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("App.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("App.Models.UsersGroups", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizGroupId");

                    b.HasKey("UserId", "QuizGroupId");

                    b.HasIndex("QuizGroupId");

                    b.ToTable("UsersGroups");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("App.Models.Answer", b =>
                {
                    b.HasOne("App.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Models.GroupsTags", b =>
                {
                    b.HasOne("App.Models.QuizGroup", "Group")
                        .WithMany("Tags")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Models.Notification", b =>
                {
                    b.HasOne("App.Models.ApplicationUser", "Issuer")
                        .WithMany("Notifications")
                        .HasForeignKey("IssuerId");

                    b.HasOne("App.Models.QuizGroup", "QuizGroup")
                        .WithMany()
                        .HasForeignKey("QuizGroupId");

                    b.HasOne("App.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId");
                });

            modelBuilder.Entity("App.Models.ProgressesAnswers", b =>
                {
                    b.HasOne("App.Models.Answer", "Answer")
                        .WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Models.QuizProgress", "Progress")
                        .WithMany("GivenAnswers")
                        .HasForeignKey("ProgressQuizId", "ProgressQuestionId", "ProgressUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Models.Question", b =>
                {
                    b.HasOne("App.Models.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Models.Quiz", b =>
                {
                    b.HasOne("App.Models.ApplicationUser", "Creator")
                        .WithMany("OwnQuizzes")
                        .HasForeignKey("CreatorId");

                    b.HasOne("App.Models.QuizGroup", "Group")
                        .WithMany("Quizzes")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Models.QuizGroup", b =>
                {
                    b.HasOne("App.Models.ApplicationUser", "Owner")
                        .WithMany("OwnGroups")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("App.Models.QuizzesTags", b =>
                {
                    b.HasOne("App.Models.Quiz", "Quiz")
                        .WithMany("Tags")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Models.QuizzesUsers", b =>
                {
                    b.HasOne("App.Models.Quiz", "Quiz")
                        .WithMany("Participants")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Models.ApplicationUser", "User")
                        .WithMany("TakenQuizzes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Models.Rating", b =>
                {
                    b.HasOne("App.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Models.Score", b =>
                {
                    b.HasOne("App.Models.Quiz", "Quiz")
                        .WithMany("Scores")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Models.ApplicationUser", "User")
                        .WithMany("Scores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("App.Models.UsersGroups", b =>
                {
                    b.HasOne("App.Models.QuizGroup", "QuizGroup")
                        .WithMany("Members")
                        .HasForeignKey("QuizGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Models.ApplicationUser", "User")
                        .WithMany("InQuizGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("App.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("App.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("App.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
