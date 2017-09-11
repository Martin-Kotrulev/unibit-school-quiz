using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace App.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizGroups_QuizGroupId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_QuizGroupId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "IsOneTime",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuizGroupId",
                table: "Quizzes");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Quizzes",
                type: "int4",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Once",
                table: "Quizzes",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_GroupId",
                table: "Quizzes",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizGroups_GroupId",
                table: "Quizzes",
                column: "GroupId",
                principalTable: "QuizGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizGroups_GroupId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_GroupId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Once",
                table: "Quizzes");

            migrationBuilder.AddColumn<bool>(
                name: "IsOneTime",
                table: "Quizzes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QuizGroupId",
                table: "Quizzes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_QuizGroupId",
                table: "Quizzes",
                column: "QuizGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizGroups_QuizGroupId",
                table: "Quizzes",
                column: "QuizGroupId",
                principalTable: "QuizGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
