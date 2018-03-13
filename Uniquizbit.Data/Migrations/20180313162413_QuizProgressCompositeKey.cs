using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Uniquizbit.Data.Migrations
{
    public partial class QuizProgressCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressAnswers_QuizProgresses_QuizProgressId",
                table: "ProgressAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses");

            migrationBuilder.DropIndex(
                name: "IX_QuizProgresses_QuizId",
                table: "QuizProgresses");

            migrationBuilder.DropIndex(
                name: "IX_QuizProgresses_UserId",
                table: "QuizProgresses");

            migrationBuilder.DropIndex(
                name: "IX_ProgressAnswers_QuizProgressId",
                table: "ProgressAnswers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuizProgresses");

            migrationBuilder.AddColumn<int>(
                name: "QuizProgressQuizId",
                table: "ProgressAnswers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuizProgressUserId",
                table: "ProgressAnswers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses",
                columns: new[] { "QuizId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressAnswers_QuizProgressQuizId_QuizProgressUserId",
                table: "ProgressAnswers",
                columns: new[] { "QuizProgressQuizId", "QuizProgressUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressAnswers_QuizProgresses_QuizProgressQuizId_QuizProgressUserId",
                table: "ProgressAnswers",
                columns: new[] { "QuizProgressQuizId", "QuizProgressUserId" },
                principalTable: "QuizProgresses",
                principalColumns: new[] { "QuizId", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressAnswers_QuizProgresses_QuizProgressQuizId_QuizProgressUserId",
                table: "ProgressAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses");

            migrationBuilder.DropIndex(
                name: "IX_ProgressAnswers_QuizProgressQuizId_QuizProgressUserId",
                table: "ProgressAnswers");

            migrationBuilder.DropColumn(
                name: "QuizProgressQuizId",
                table: "ProgressAnswers");

            migrationBuilder.DropColumn(
                name: "QuizProgressUserId",
                table: "ProgressAnswers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QuizProgresses",
                nullable: false,
                defaultValue: 0)
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

            migrationBuilder.CreateIndex(
                name: "IX_ProgressAnswers_QuizProgressId",
                table: "ProgressAnswers",
                column: "QuizProgressId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressAnswers_QuizProgresses_QuizProgressId",
                table: "ProgressAnswers",
                column: "QuizProgressId",
                principalTable: "QuizProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
