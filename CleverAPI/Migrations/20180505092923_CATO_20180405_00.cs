using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CleverAPI.Migrations
{
    public partial class CATO_20180405_00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaType",
                table: "CATO",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EgovId",
                table: "CATO",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Parent",
                table: "CATO",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaType",
                table: "CATO");

            migrationBuilder.DropColumn(
                name: "EgovId",
                table: "CATO");

            migrationBuilder.DropColumn(
                name: "Parent",
                table: "CATO");
        }
    }
}
