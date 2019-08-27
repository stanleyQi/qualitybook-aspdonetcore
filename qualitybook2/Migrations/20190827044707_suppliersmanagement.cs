using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace qualitybook2.Migrations
{
    public partial class suppliersmanagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberHome",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberMobile",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberWork",
                table: "Suppliers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberHome",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberMobile",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberWork",
                table: "Suppliers");
        }
    }
}
