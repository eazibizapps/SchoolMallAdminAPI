using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class ScoolGrades1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GrageId",
                table: "ScoolGrades",
                newName: "GrageCode");

            migrationBuilder.AddColumn<string>(
                name: "Type006",
                table: "ScoolGrades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type006",
                table: "ScoolGrades");

            migrationBuilder.RenameColumn(
                name: "GrageCode",
                table: "ScoolGrades",
                newName: "GrageId");
        }
    }
}
