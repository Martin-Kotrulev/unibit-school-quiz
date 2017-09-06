using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace App.Migrations
{
    public partial class UpdatedProgresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuizProgresses");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "QuizProgresses");

            migrationBuilder.AddColumn<int>(
                name: "QuizProgressQuestionId",
                table: "Answers",
                type: "int4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuizProgressQuizId",
                table: "Answers",
                type: "int4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuizProgressUserId",
                table: "Answers",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses",
                columns: new[] { "QuizId", "QuestionId", "UserId" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuizProgresses_QuizProgressQuizId_QuizProgressQuestionId_QuizProgressUserId",
                table: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses");

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

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QuizProgresses",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "QuizProgresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizProgresses",
                table: "QuizProgresses",
                column: "Id");
        }
    }
}
