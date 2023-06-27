using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JWForm.Migrations
{
    /// <inheritdoc />
    public partial class MudandoParaPostgresql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publicadores",
                columns: table => new
                {
                    PublicadorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    GrupoDeCampo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicadores", x => x.PublicadorId);
                });

            migrationBuilder.CreateTable(
                name: "Relatorios",
                columns: table => new
                {
                    RelatorioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: false),
                    Videos = table.Column<int>(type: "integer", nullable: false),
                    Publicacoes = table.Column<int>(type: "integer", nullable: false),
                    Revisitas = table.Column<int>(type: "integer", nullable: false),
                    EstudosBiblicos = table.Column<int>(type: "integer", nullable: false),
                    Horas = table.Column<int>(type: "integer", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PublicadorId = table.Column<int>(type: "integer", nullable: false)
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
