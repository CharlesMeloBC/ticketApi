using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ticketApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateDeletAtInModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAT",
                table: "Tickets",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAT",
                table: "Tickets");
        }
    }
}
