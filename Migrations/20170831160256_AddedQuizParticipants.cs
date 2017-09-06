using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace App.Migrations
{
    public partial class AddedQuizParticipants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_AspNetUsers_ApplicationUserId",
                table: "Quizes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_AspNetUsers_ApplicationUserId1",
                table: "Quizes");

            migrationBuilder.DropIndex(
                name: "IX_Quizes_ApplicationUserId",
                table: "Quizes");

            migrationBuilder.DropIndex(
                name: "IX_Quizes_ApplicationUserId1",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Quizes");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Quizes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "AspNetUsers",
                type: "int4",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuizesUsers",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int4", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizesUsers", x => new { x.QuizId, x.UserId });
                    table.ForeignKey(
                        name: "FK_QuizesUsers_Quizes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizesUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_CreatorId",
                table: "Quizes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_QuizId",
                table: "AspNetUsers",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizesUsers_UserId",
                table: "QuizesUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Quizes_QuizId",
                table: "AspNetUsers",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_AspNetUsers_CreatorId",
                table: "Quizes",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Quizes_QuizId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_AspNetUsers_CreatorId",
                table: "Quizes");

            migrationBuilder.DropTable(
                name: "QuizesUsers");

            migrationBuilder.DropIndex(
                name: "IX_Quizes_CreatorId",
                table: "Quizes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_QuizId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Quizes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Quizes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_ApplicationUserId",
                table: "Quizes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_ApplicationUserId1",
                table: "Quizes",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_AspNetUsers_ApplicationUserId",
                table: "Quizes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_AspNetUsers_ApplicationUserId1",
                table: "Quizes",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
