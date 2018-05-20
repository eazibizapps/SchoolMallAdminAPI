using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class ScoolGradesList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GrageCode",
                table: "ScoolGrades",
                newName: "GradeCode");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "SuppliersAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "SupplierProducts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "SuppliersAddress");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "SupplierProducts");

            migrationBuilder.RenameColumn(
                name: "GradeCode",
                table: "ScoolGrades",
                newName: "GrageCode");
        }
    }
}
