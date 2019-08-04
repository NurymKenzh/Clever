using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CleverAPI.Migrations
{
    public partial class AirContaminant_20180503_00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirContaminant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ApproximateSafeExposureLevel = table.Column<decimal>(nullable: true),
                    HazardClass = table.Column<int>(nullable: true),
                    MaximumPermissibleConcentrationDailyAverage = table.Column<decimal>(nullable: true),
                    MaximumPermissibleConcentrationOneTimeMaximum = table.Column<decimal>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NumberCAS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirContaminant", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirContaminant");
        }
    }
}
