using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CleverAPI.Migrations
{
    public partial class CompanyKK_20180604_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyKK_NameRU",
                table: "CompanyKK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CompanyKK_NameRU",
                table: "CompanyKK",
                column: "NameRU");
        }
    }
}
