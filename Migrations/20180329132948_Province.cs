using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class Province : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Posta_Province",
                table: "SchoolsAddress",
                newName: "Type006");

            migrationBuilder.RenameColumn(
                name: "Physical_Province",
                table: "SchoolsAddress",
                newName: "Province");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type006",
                table: "SchoolsAddress",
                newName: "Posta_Province");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "SchoolsAddress",
                newName: "Physical_Province");
        }
    }
}
