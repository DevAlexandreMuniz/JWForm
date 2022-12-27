using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWForm.Migrations
{
    /// <inheritdoc />
    public partial class TipoDoPublicador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Publicadores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Publicadores");
        }
    }
}
