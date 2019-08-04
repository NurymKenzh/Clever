using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CleverAPI.Migrations
{
    public partial class CompanyKK_20180604_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CompanyKK_NameRU",
                table: "CompanyKK",
                column: "NameRU");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyKK_NameRU",
                table: "CompanyKK");
        }
    }
}
