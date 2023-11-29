using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoLocalBack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateAndRemoveVideocardFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnergyConsumption",
                table: "Videocard");

            migrationBuilder.DropColumn(
                name: "FanSpeed",
                table: "Videocard");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Monitorings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Monitorings");

            migrationBuilder.AddColumn<double>(
                name: "EnergyConsumption",
                table: "Videocard",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "FanSpeed",
                table: "Videocard",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
