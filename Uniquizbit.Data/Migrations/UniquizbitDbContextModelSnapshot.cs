﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Uniquizbit.Data;

namespace Uniquizbit.Data.Migrations
{
    [DbContext(typeof(UniquizbitDbContext))]
    partial class UniquizbitDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

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

            modelBuilder.Entity("Uniquizbit.Data.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatorId");

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

                    b.HasIndex("CreatorId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuizId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.GroupsTags", b =>
                {
                    b.Property<int>("GroupId");

                    b.Property<int>("TagId");

                    b.HasKey("GroupId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("GroupsTags");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.GroupSubscription", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizGroupId");

                    b.HasKey("UserId", "QuizGroupId");

                    b.ToTable("GroupSubscriptions");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Notification", b =>
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

            modelBuilder.Entity("Uniquizbit.Data.Models.ProgressAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerId");

                    b.Property<bool>("IsChecked");

                    b.Property<int>("QuestionId");

                    b.Property<int>("QuizProgressId");

                    b.Property<int?>("QuizProgressQuizId");

                    b.Property<string>("QuizProgressUserId");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("QuizProgressQuizId", "QuizProgressUserId");

                    b.ToTable("ProgressAnswers");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatorId");

                    b.Property<bool>("IsMultiselect");

                    b.Property<int>("MaxAnswers");

                    b.Property<int>("QuizId");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<DateTime?>("Ends");

                    b.Property<int?>("GroupId");

                    b.Property<bool>("Locked");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("Once");

                    b.Property<string>("Password");

                    b.Property<bool>("Published");

                    b.Property<DateTime?>("Starts");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("GroupId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.QuizGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CreatorId");

                    b.Property<string>("CreatorName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("QuizGroups");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.QuizProgress", b =>
                {
                    b.Property<int>("QuizId");

                    b.Property<string>("UserId");

                    b.Property<string>("AnswersOrder");

                    b.Property<string>("QuestionsOrder");

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("QuizId", "UserId");

                    b.ToTable("QuizProgresses");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.QuizSubscription", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizId");

                    b.HasKey("UserId", "QuizId");

                    b.ToTable("QuizSubscriptions");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.QuizzesTags", b =>
                {
                    b.Property<int>("QuizId");

                    b.Property<int>("TagId");

                    b.HasKey("QuizId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("QuizzesTags");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.QuizzesUsers", b =>
                {
                    b.Property<int>("QuizId");

                    b.Property<string>("UserId");

                    b.Property<bool>("Finished");

                    b.HasKey("QuizId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("QuizzesUsers");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Rating", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizId");

                    b.Property<double>("Value");

                    b.HasKey("UserId", "QuizId");

                    b.HasIndex("QuizId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Score", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizId");

                    b.Property<DateTime>("ScoredAt");

                    b.Property<double>("Value");

                    b.HasKey("UserId", "QuizId");

                    b.HasIndex("QuizId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Tag", b =>
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

            modelBuilder.Entity("Uniquizbit.Data.Models.User", b =>
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

            modelBuilder.Entity("Uniquizbit.Data.Models.UsersGroups", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("QuizGroupId");

                    b.HasKey("UserId", "QuizGroupId");

                    b.HasIndex("QuizGroupId");

                    b.ToTable("UsersGroups");
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
                    b.HasOne("Uniquizbit.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.User")
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

                    b.HasOne("Uniquizbit.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Answer", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("Uniquizbit.Data.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Uniquizbit.Data.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.GroupsTags", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.QuizGroup", "Group")
                        .WithMany("Tags")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Uniquizbit.Data.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Notification", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.User", "Issuer")
                        .WithMany("Notifications")
                        .HasForeignKey("IssuerId");

                    b.HasOne("Uniquizbit.Data.Models.QuizGroup", "QuizGroup")
                        .WithMany()
                        .HasForeignKey("QuizGroupId");

                    b.HasOne("Uniquizbit.Data.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.ProgressAnswer", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.Answer", "Answer")
                        .WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Uniquizbit.Data.Models.QuizProgress", "QuizProgress")
                        .WithMany("GivenAnswers")
                        .HasForeignKey("QuizProgressQuizId", "QuizProgressUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Question", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("Uniquizbit.Data.Models.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Quiz", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.User", "Creator")
                        .WithMany("OwnQuizzes")
                        .HasForeignKey("CreatorId");

                    b.HasOne("Uniquizbit.Data.Models.QuizGroup", "Group")
                        .WithMany("Quizzes")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.QuizGroup", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.User", "Creator")
                        .WithMany("OwnGroups")
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.QuizzesTags", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.Quiz", "Quiz")
                        .WithMany("Tags")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Uniquizbit.Data.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.QuizzesUsers", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.Quiz", "Quiz")
                        .WithMany("Participants")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Uniquizbit.Data.Models.User", "User")
                        .WithMany("TakenQuizzes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Rating", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Uniquizbit.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.Score", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.Quiz", "Quiz")
                        .WithMany("Scores")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Uniquizbit.Data.Models.User", "User")
                        .WithMany("Scores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Uniquizbit.Data.Models.UsersGroups", b =>
                {
                    b.HasOne("Uniquizbit.Data.Models.QuizGroup", "QuizGroup")
                        .WithMany("Members")
                        .HasForeignKey("QuizGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Uniquizbit.Data.Models.User", "User")
                        .WithMany("InQuizGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
