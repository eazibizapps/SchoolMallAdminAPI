using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class SchoolsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Butterfly = table.Column<int>(nullable: false),
                    Category = table.Column<string>(maxLength: 10, nullable: true),
                    Cover_letter = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Forum_area = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Letter_file = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    PeriodId = table.Column<int>(nullable: false),
                    Principal = table.Column<string>(nullable: true),
                    Principal_secretary = table.Column<string>(nullable: true),
                    Reg_number = table.Column<string>(nullable: true),
                    Representative = table.Column<string>(nullable: true),
                    Signature = table.Column<string>(nullable: true),
                    Store_learners = table.Column<string>(nullable: true),
                    Store_school = table.Column<string>(nullable: true),
                    Type = table.Column<string>(maxLength: 10, nullable: true),
                    Type002 = table.Column<string>(maxLength: 3, nullable: true),
                    Type003 = table.Column<string>(maxLength: 3, nullable: true),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
