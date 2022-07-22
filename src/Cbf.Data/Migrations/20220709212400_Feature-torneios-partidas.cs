using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cbf.Data.Migrations
{
    public partial class Featuretorneiospartidas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeCasaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeVisitanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TorneioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataPartida = table.Column<DateTime>(type: "datetime", nullable: false),
                    Resultado = table.Column<string>(type: "varchar(5)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidas_Torneios_TorneioId",
                        column: x => x.TorneioId,
                        principalTable: "Torneios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_TorneioId",
                table: "Partidas",
                column: "TorneioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partidas");
        }
    }
}
