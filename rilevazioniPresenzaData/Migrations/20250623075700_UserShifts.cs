using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rilevazioniPresenzaData.Migrations
{
    /// <inheritdoc />
    public partial class UserShifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserShifts",
                columns: table => new
                {
                    IdMatricola = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Giorno = table.Column<int>(type: "int", nullable: false),
                    T1 = table.Column<TimeOnly>(type: "time", nullable: true),
                    FT1 = table.Column<TimeOnly>(type: "time", nullable: true),
                    T2 = table.Column<TimeOnly>(type: "time", nullable: true),
                    FT2 = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserShifts", x => new { x.IdMatricola, x.Giorno });
                    table.ForeignKey(
                        name: "FK_UserShifts_Users_IdMatricola",
                        column: x => x.IdMatricola,
                        principalTable: "Users",
                        principalColumn: "Matricola",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserShifts");
        }
    }
}
