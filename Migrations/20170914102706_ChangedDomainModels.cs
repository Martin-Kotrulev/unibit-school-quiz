using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace App.Migrations
{
    public partial class ChangedDomainModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizGroups_AspNetUsers_OwnerId",
                table: "QuizGroups");

            migrationBuilder.DropIndex(
                name: "IX_QuizGroups_OwnerId",
                table: "QuizGroups");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "QuizGroups");

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "Quizzes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "QuizGroups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "QuizGroups",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizGroups_CreatorId",
                table: "QuizGroups",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizGroups_AspNetUsers_CreatorId",
                table: "QuizGroups",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizGroups_AspNetUsers_CreatorId",
                table: "QuizGroups");

            migrationBuilder.DropIndex(
                name: "IX_QuizGroups_CreatorId",
                table: "QuizGroups");

            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "QuizGroups");

            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "QuizGroups");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "QuizGroups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizGroups_OwnerId",
                table: "QuizGroups",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizGroups_AspNetUsers_OwnerId",
                table: "QuizGroups",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
