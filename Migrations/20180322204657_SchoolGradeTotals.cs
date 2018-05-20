using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class SchoolGradeTotals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ScoolGrades",
                newName: "ScoolGradesId");

            migrationBuilder.CreateTable(
                name: "SchoolGradeTotals",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoOffClasses = table.Column<int>(nullable: false),
                    NoOffLearners = table.Column<int>(nullable: false),
                    NoOffParticipation = table.Column<int>(nullable: false),
                    PeriodId = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false),
                    ScoolGradesId = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolGradeTotals", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolGradeTotals");

            migrationBuilder.RenameColumn(
                name: "ScoolGradesId",
                table: "ScoolGrades",
                newName: "ID");
        }
    }
}
