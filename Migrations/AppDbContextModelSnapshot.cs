﻿// <auto-generated />
using App.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace App.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("App.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FromQuestionId");

                    b.Property<bool>("IsRight");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FromQuestionId");

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

            modelBuilder.Entity("App.Models.GroupSubscribtion", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizGroupId");

                    b.HasKey("UserId", "QuizGroupId");

                    b.ToTable("GroupSubscribtions");
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

            modelBuilder.Entity("App.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaxAnswers");

                    b.Property<int?>("QuizId");

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

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("ApplicationUserId1");

                    b.Property<DateTime>("EndDateTime");

                    b.Property<bool>("IsOneTime");

                    b.Property<bool>("Locked");

                    b.Property<string>("Password");

                    b.Property<int?>("QuizGroupId");

                    b.Property<DateTime>("StartDateTime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ApplicationUserId1");

                    b.HasIndex("QuizGroupId");

                    b.ToTable("Quizes");
                });

            modelBuilder.Entity("App.Models.QuizGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerId");

                    b.Property<int>("QuestionId");

                    b.Property<int>("QuizId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("Id");

                    b.ToTable("QuizProgresses");
                });

            modelBuilder.Entity("App.Models.QuizSubscription", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizId");

                    b.HasKey("UserId", "QuizId");

                    b.ToTable("QuizSubscribtions");
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("QuizId");

                    b.Property<DateTime>("ScoredAt");

                    b.Property<string>("UserId");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.HasIndex("UserId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("App.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int?>("QuizGroupId");

                    b.Property<int?>("QuizId");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("QuizGroupId");

                    b.HasIndex("QuizId");

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
                    b.HasOne("App.Models.Question", "FromQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("FromQuestionId");
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

            modelBuilder.Entity("App.Models.Question", b =>
                {
                    b.HasOne("App.Models.Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId");
                });

            modelBuilder.Entity("App.Models.Quiz", b =>
                {
                    b.HasOne("App.Models.ApplicationUser")
                        .WithMany("OwnQuizes")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("App.Models.ApplicationUser")
                        .WithMany("TakenQuizes")
                        .HasForeignKey("ApplicationUserId1");

                    b.HasOne("App.Models.QuizGroup", "QuizGroup")
                        .WithMany("Quizes")
                        .HasForeignKey("QuizGroupId");
                });

            modelBuilder.Entity("App.Models.QuizGroup", b =>
                {
                    b.HasOne("App.Models.ApplicationUser", "Owner")
                        .WithMany("OwnGroups")
                        .HasForeignKey("OwnerId");
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
                        .HasForeignKey("QuizId");

                    b.HasOne("App.Models.ApplicationUser", "User")
                        .WithMany("Scores")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("App.Models.Tag", b =>
                {
                    b.HasOne("App.Models.QuizGroup")
                        .WithMany("Tags")
                        .HasForeignKey("QuizGroupId");

                    b.HasOne("App.Models.Quiz")
                        .WithMany("Tags")
                        .HasForeignKey("QuizId");
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
