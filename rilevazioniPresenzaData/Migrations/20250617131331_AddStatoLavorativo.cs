using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rilevazioniPresenzaData.Migrations
{
    /// <inheritdoc />
    public partial class AddStatoLavorativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stato_Lavorativo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stato_Lavorativo",
                table: "Users");
        }
    }
}
