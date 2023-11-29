using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoLocalBack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletName = table.Column<string>(type: "text", nullable: false),
                    WalletCoinName = table.Column<string>(type: "text", nullable: false),
                    WalletAddress = table.Column<string>(type: "text", nullable: false),
                    RigName = table.Column<string>(type: "text", nullable: false),
                    ServerName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videocard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    CCDType = table.Column<int>(type: "integer", nullable: false),
                    CardManufacturer = table.Column<string>(type: "text", nullable: false),
                    CCDModel = table.Column<string>(type: "text", nullable: false),
                    GPUFrequency = table.Column<string>(type: "text", nullable: false),
                    MemoryFrequency = table.Column<string>(type: "text", nullable: false),
                    EnergyConsumption = table.Column<double>(type: "double precision", nullable: false),
                    FanSpeed = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videocard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monitorings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CurrentHashrate = table.Column<double>(type: "double precision", nullable: false),
                    GPUTemperature = table.Column<double>(type: "double precision", nullable: false),
                    FanRPM = table.Column<double>(type: "double precision", nullable: false),
                    EnergyConsumption = table.Column<double>(type: "double precision", nullable: false),
                    VideocardId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitorings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monitorings_Videocard_VideocardId",
                        column: x => x.VideocardId,
                        principalTable: "Videocard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monitorings_VideocardId",
                table: "Monitorings",
                column: "VideocardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitorings");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Videocard");
        }
    }
}
