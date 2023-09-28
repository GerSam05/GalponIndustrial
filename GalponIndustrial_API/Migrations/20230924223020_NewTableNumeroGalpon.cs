using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalponIndustrial_API.Migrations
{
    public partial class NewTableNumeroGalpon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroGalpones",
                columns: table => new
                {
                    NroGalpon = table.Column<int>(type: "int", nullable: false),
                    GalponId = table.Column<int>(type: "int", nullable: false),
                    Detalles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroGalpones", x => x.NroGalpon);
                    table.ForeignKey(
                        name: "FK_NumeroGalpones_Galpons_GalponId",
                        column: x => x.GalponId,
                        principalTable: "Galpons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroGalpones_GalponId",
                table: "NumeroGalpones",
                column: "GalponId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroGalpones");
        }
    }
}
