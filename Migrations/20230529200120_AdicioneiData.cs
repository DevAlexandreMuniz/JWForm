using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWForm.Migrations
{
    /// <inheritdoc />
    public partial class AdicioneiData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mes",
                table: "Relatorios",
                newName: "Data");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Relatorios",
                newName: "Mes");
        }
    }
}
