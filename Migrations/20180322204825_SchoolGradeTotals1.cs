using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class SchoolGradeTotals1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScoolGradesId",
                table: "ScoolGrades",
                newName: "SchoolGradeId");

            migrationBuilder.RenameColumn(
                name: "ScoolGradesId",
                table: "SchoolGradeTotals",
                newName: "SchoolGradeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchoolGradeId",
                table: "ScoolGrades",
                newName: "ScoolGradesId");

            migrationBuilder.RenameColumn(
                name: "SchoolGradeId",
                table: "SchoolGradeTotals",
                newName: "ScoolGradesId");
        }
    }
}
