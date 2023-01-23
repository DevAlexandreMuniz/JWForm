using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWForm.Migrations
{
    /// <inheritdoc />
    public partial class removerMes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mes",
                table: "Relatorios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mes",
                table: "Relatorios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
