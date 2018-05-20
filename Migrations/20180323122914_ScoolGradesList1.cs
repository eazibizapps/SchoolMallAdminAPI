using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class ScoolGradesList1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoolGradesList",
                columns: table => new
                {
                    ScoolGradesListID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PeriodId = table.Column<int>(nullable: false),
                    ProducListDescription = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    SchoolGradeId = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false),
                    SuppliersId = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoolGradesList", x => x.ScoolGradesListID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoolGradesList");
        }
    }
}
