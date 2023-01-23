using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWForm.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarMes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Mes",
                table: "Relatorios",
                type: "datetime2",
                maxLength: 50,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mes",
                table: "Relatorios");
        }
    }
}
