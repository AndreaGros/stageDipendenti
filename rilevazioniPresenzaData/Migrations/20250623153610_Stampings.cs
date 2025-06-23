using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rilevazioniPresenzaData.Migrations
{
    /// <inheritdoc />
    public partial class Stampings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stampings",
                columns: table => new
                {
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    Orario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdMatricola = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stampings", x => new { x.ShiftType, x.Orario });
                    table.ForeignKey(
                        name: "FK_Stampings_Users_IdMatricola",
                        column: x => x.IdMatricola,
                        principalTable: "Users",
                        principalColumn: "Matricola",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stampings_IdMatricola",
                table: "Stampings",
                column: "IdMatricola");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stampings");
        }
    }
}
