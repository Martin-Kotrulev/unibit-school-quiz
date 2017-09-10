using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace App.Migrations
{
    public partial class QuizProgressesM2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuizProgresses_QuizProgressQuizId_QuizProgressQuestionId_QuizProgressUserId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuizProgressQuizId_QuizProgressQuestionId_QuizProgressUserId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuizProgressQuestionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuizProgressQuizId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuizProgressUserId",
                table: "Answers");

            migrationBuilder.CreateTable(
                name: "ProgressesAnswers",
                columns: table => new
                {
                    ProgressId = table.Column<int>(type: "int4", nullable: false),
                    AnswerId = table.Column<int>(type: "int4", nullable: false),
                    ProgressQuestionId = table.Column<int>(type: "int4", nullable: true),
                    ProgressQuizId = table.Column<int>(type: "int4", nullable: true),
                    ProgressUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressesAnswers", x => new { x.ProgressId, x.AnswerId });
                    table.ForeignKey(
                        name: "FK_ProgressesAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgressesAnswers_QuizProgresses_ProgressQuizId_ProgressQuestionId_ProgressUserId",
                        columns: x => new { x.ProgressQuizId, x.ProgressQuestionId, x.ProgressUserId },
                        principalTable: "QuizProgresses",
                        principalColumns: new[] { "QuizId", "QuestionId", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressesAnswers_AnswerId",
                table: "ProgressesAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressesAnswers_ProgressQuizId_ProgressQuestionId_ProgressUserId",
                table: "ProgressesAnswers",
                columns: new[] { "ProgressQuizId", "ProgressQuestionId", "ProgressUserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressesAnswers");

            migrationBuilder.AddColumn<int>(
                name: "QuizProgressQuestionId",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuizProgressQuizId",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuizProgressUserId",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuizProgressQuizId_QuizProgressQuestionId_QuizProgressUserId",
                table: "Answers",
                columns: new[] { "QuizProgressQuizId", "QuizProgressQuestionId", "QuizProgressUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuizProgresses_QuizProgressQuizId_QuizProgressQuestionId_QuizProgressUserId",
                table: "Answers",
                columns: new[] { "QuizProgressQuizId", "QuizProgressQuestionId", "QuizProgressUserId" },
                principalTable: "QuizProgresses",
                principalColumns: new[] { "QuizId", "QuestionId", "UserId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
