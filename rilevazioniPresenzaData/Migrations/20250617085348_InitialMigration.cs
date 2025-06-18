using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rilevazioniPresenzaData.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Matricola = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Badge = table.Column<int>(type: "int", nullable: false),
                    Nominativo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sesso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stato_Civile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_Nascita = table.Column<DateOnly>(type: "date", nullable: false),
                    Citta_Nascita = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provincia_Nascita = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stato_Nascita = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indirizzo_Residenza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provincia_Residenza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stato_Residenza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codice_Fiscale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero_Telefono = table.Column<int>(type: "int", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Matricola);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
