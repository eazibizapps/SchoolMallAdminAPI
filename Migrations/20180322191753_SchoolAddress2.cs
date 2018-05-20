using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class SchoolAddress2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Posta_PostalCode",
                table: "SchoolsAddress",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Posta_PostalCode",
                table: "SchoolsAddress",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
