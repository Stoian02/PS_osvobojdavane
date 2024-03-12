using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserForeignKeyToLogEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    _id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    _fakNum = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    Expires = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Level = table.Column<string>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "_id", "Email", "Expires", "Name", "Password", "Role", "_fakNum" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2034, 3, 12, 17, 20, 40, 754, DateTimeKind.Local).AddTicks(7606), "John Doe", "$2a$11$VRLMO8RhK05p6WXnzpyRRuotVxfEL0glpWnEElZTbxlzHby5f623e", 1, "000" },
                    { 2, "", new DateTime(2025, 3, 12, 17, 20, 40, 879, DateTimeKind.Local).AddTicks(7713), "Doey", "$2a$11$.u0yK4O0inbgUZATH6IFp...cnlJSXIPo/iUYYN4GxAtpVO0Phwlu", 2, "112" },
                    { 3, "", new DateTime(2025, 3, 12, 17, 20, 41, 9, DateTimeKind.Local).AddTicks(3073), "Kris", "$2a$11$QT4aScYG3lFlHKoOFxQT6eQiGbogXGjheQ5qAOHI3sccSA34VvnWu", 4, "221" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_UserId",
                table: "LogEntries",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
