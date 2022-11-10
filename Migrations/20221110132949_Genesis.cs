using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWForm.Migrations
{
    /// <inheritdoc />
    public partial class Genesis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publicadores",
                columns: table => new
                {
                    PublicadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GrupoDeCampo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnviouORelatorio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicadores", x => x.PublicadorId);
                });

            migrationBuilder.CreateTable(
                name: "Relatorios",
                columns: table => new
                {
                    RelatorioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Videos = table.Column<int>(type: "int", nullable: false),
                    Publicacoes = table.Column<int>(type: "int", nullable: false),
                    Revisitas = table.Column<int>(type: "int", nullable: false),
                    EstudosBiblicos = table.Column<int>(type: "int", nullable: false),
                    Horas = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PublicadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatorios", x => x.RelatorioId);
                    table.ForeignKey(
                        name: "FK_Relatorios_Publicadores_PublicadorId",
                        column: x => x.PublicadorId,
                        principalTable: "Publicadores",
                        principalColumn: "PublicadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relatorios_PublicadorId",
                table: "Relatorios",
                column: "PublicadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relatorios");

            migrationBuilder.DropTable(
                name: "Publicadores");
        }
    }
}
