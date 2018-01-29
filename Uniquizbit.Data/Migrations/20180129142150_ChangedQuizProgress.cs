using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Uniquizbit.Data.Migrations
{
    public partial class ChangedQuizProgress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressesAnswers_QuizProgresses_ProgressQuizId_ProgressUserId",
                table: "ProgressesAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses");

            migrationBuilder.DropIndex(
                name: "IX_ProgressesAnswers_ProgressQuizId_ProgressUserId",
                table: "ProgressesAnswers");

            migrationBuilder.DropColumn(
                name: "ProgressQuizId",
                table: "ProgressesAnswers");

            migrationBuilder.DropColumn(
                name: "ProgressUserId",
                table: "ProgressesAnswers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QuizProgresses",
                nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuizProgresses_QuizId",
                table: "QuizProgresses",
                column: "QuizId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizProgresses_UserId",
                table: "QuizProgresses",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressesAnswers_QuizProgresses_ProgressId",
                table: "ProgressesAnswers",
                column: "ProgressId",
                principalTable: "QuizProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressesAnswers_QuizProgresses_ProgressId",
                table: "ProgressesAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses");

            migrationBuilder.DropIndex(
                name: "IX_QuizProgresses_QuizId",
                table: "QuizProgresses");

            migrationBuilder.DropIndex(
                name: "IX_QuizProgresses_UserId",
                table: "QuizProgresses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuizProgresses");

            migrationBuilder.AddColumn<int>(
                name: "ProgressQuizId",
                table: "ProgressesAnswers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressUserId",
                table: "ProgressesAnswers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses",
                columns: new[] { "QuizId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressesAnswers_ProgressQuizId_ProgressUserId",
                table: "ProgressesAnswers",
                columns: new[] { "ProgressQuizId", "ProgressUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressesAnswers_QuizProgresses_ProgressQuizId_ProgressUserId",
                table: "ProgressesAnswers",
                columns: new[] { "ProgressQuizId", "ProgressUserId" },
                principalTable: "QuizProgresses",
                principalColumns: new[] { "QuizId", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
