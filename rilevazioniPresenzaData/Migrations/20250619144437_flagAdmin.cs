﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rilevazioniPresenzaData.Migrations
{
    /// <inheritdoc />
    public partial class flagAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Users");
        }
    }
}
