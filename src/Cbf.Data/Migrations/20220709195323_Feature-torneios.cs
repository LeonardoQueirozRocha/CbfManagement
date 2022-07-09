using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cbf.Data.Migrations
{
    public partial class Featuretorneios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Torneios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeTorneio",
                columns: table => new
                {
                    TimesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TorneiosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTorneio", x => new { x.TimesId, x.TorneiosId });
                    table.ForeignKey(
                        name: "FK_TimeTorneio_Times_TimesId",
                        column: x => x.TimesId,
                        principalTable: "Times",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeTorneio_Torneios_TorneiosId",
                        column: x => x.TorneiosId,
                        principalTable: "Torneios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeTorneio_TorneiosId",
                table: "TimeTorneio",
                column: "TorneiosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeTorneio");

            migrationBuilder.DropTable(
                name: "Torneios");
        }
    }
}
