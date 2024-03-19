using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weatherman.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createtabledailyTemperature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyTemperatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TemperatureC = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTemperatures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTemperatures_Date",
                table: "DailyTemperatures",
                column: "Date",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTemperatures");
        }
    }
}
