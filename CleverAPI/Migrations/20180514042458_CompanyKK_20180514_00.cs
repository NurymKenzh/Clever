using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CleverAPI.Migrations
{
    public partial class CompanyKK_20180514_00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyKK",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActivityKindKK = table.Column<string>(nullable: true),
                    ActivityKindRU = table.Column<string>(nullable: true),
                    BIN = table.Column<string>(nullable: true),
                    CATO = table.Column<string>(nullable: true),
                    DateRegister = table.Column<DateTime>(nullable: true),
                    HeadName = table.Column<string>(nullable: true),
                    KRP = table.Column<string>(nullable: true),
                    KRPNameKK = table.Column<string>(nullable: true),
                    KRPNameRU = table.Column<string>(nullable: true),
                    LegalAddress = table.Column<string>(nullable: true),
                    LocalityKK = table.Column<string>(nullable: true),
                    LocalityRU = table.Column<string>(nullable: true),
                    NameKK = table.Column<string>(nullable: true),
                    NameRU = table.Column<string>(nullable: true),
                    OKED = table.Column<string>(nullable: true),
                    OKEDSecondary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyKK", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyKK");
        }
    }
}
