using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Uniquizbit.Data.Migrations
{
    public partial class ProgressAnswerChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressAnswer_Answers_AnswerId",
                table: "ProgressAnswer");

            migrationBuilder.DropTable(
                name: "ProgressesAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgressAnswer",
                table: "ProgressAnswer");

            migrationBuilder.RenameTable(
                name: "ProgressAnswer",
                newName: "ProgressAnswers");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "ProgressAnswers",
                newName: "QuizProgressId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressAnswer_AnswerId",
                table: "ProgressAnswers",
                newName: "IX_ProgressAnswers_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgressAnswers",
                table: "ProgressAnswers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressAnswers_QuizProgressId",
                table: "ProgressAnswers",
                column: "QuizProgressId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressAnswers_Answers_AnswerId",
                table: "ProgressAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressAnswers_QuizProgresses_QuizProgressId",
                table: "ProgressAnswers",
                column: "QuizProgressId",
                principalTable: "QuizProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressAnswers_Answers_AnswerId",
                table: "ProgressAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressAnswers_QuizProgresses_QuizProgressId",
                table: "ProgressAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgressAnswers",
                table: "ProgressAnswers");

            migrationBuilder.DropIndex(
                name: "IX_ProgressAnswers_QuizProgressId",
                table: "ProgressAnswers");

            migrationBuilder.RenameTable(
                name: "ProgressAnswers",
                newName: "ProgressAnswer");

            migrationBuilder.RenameColumn(
                name: "QuizProgressId",
                table: "ProgressAnswer",
                newName: "QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressAnswers_AnswerId",
                table: "ProgressAnswer",
                newName: "IX_ProgressAnswer_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgressAnswer",
                table: "ProgressAnswer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProgressesAnswers",
                columns: table => new
                {
                    ProgressId = table.Column<int>(nullable: false),
                    ProgressAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressesAnswers", x => new { x.ProgressId, x.ProgressAnswerId });
                    table.ForeignKey(
                        name: "FK_ProgressesAnswers_ProgressAnswer_ProgressAnswerId",
                        column: x => x.ProgressAnswerId,
                        principalTable: "ProgressAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgressesAnswers_QuizProgresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "QuizProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressesAnswers_ProgressAnswerId",
                table: "ProgressesAnswers",
                column: "ProgressAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressAnswer_Answers_AnswerId",
                table: "ProgressAnswer",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
