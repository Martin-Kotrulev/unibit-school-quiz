using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace App.Migrations
{
    public partial class CascadeQuizDeletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizGroups_QuizGroupId",
                table: "Quizzes");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizGroups_QuizGroupId",
                table: "Quizzes",
                column: "QuizGroupId",
                principalTable: "QuizGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizGroups_QuizGroupId",
                table: "Quizzes");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizGroups_QuizGroupId",
                table: "Quizzes",
                column: "QuizGroupId",
                principalTable: "QuizGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
