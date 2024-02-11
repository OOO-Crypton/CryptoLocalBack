using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoLocalBack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSettins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Monitorings",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "VideocardSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PowerLimit = table.Column<double>(type: "double precision", nullable: false),
                    CoreLimit = table.Column<double>(type: "double precision", nullable: false),
                    MemoryLimit = table.Column<double>(type: "double precision", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideocardSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideocardSettings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Monitorings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
