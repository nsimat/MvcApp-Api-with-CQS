using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbApiwithCQS.Domain.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2(7)", nullable: false, defaultValueSql: "sysdatetime()"),
                    Cloturee = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "Cloturee", "DateCreation", "Titre" },
                values: new object[] { 1, true, new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design d'un site e-commerce avec l'outil figma" });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "DateCreation", "Titre" },
                values: new object[] { 2, new DateTime(2026, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Développement d'un blog avec Angular 22 et ASP.NET Core 10" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");
        }
    }
}
