using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace qualitybook2.Migrations
{
    public partial class modifyordermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderTotalPrice",
                table: "Orders",
                newName: "Subtotal");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GST",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GrandTotal",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            //migrationBuilder.AddColumn<string>(
            //    name: "Address",
            //    table: "AspNetUsers",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GST",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GrandTotal",
                table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "Address",
            //    table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "Orders",
                newName: "OrderTotalPrice");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Orders",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Orders",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Orders",
                nullable: false,
                defaultValue: "");
        }
    }
}
