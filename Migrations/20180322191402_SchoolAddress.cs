using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class SchoolAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchoolsAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Physical_AddressLine1 = table.Column<string>(nullable: true),
                    Physical_AddressLine2 = table.Column<string>(nullable: true),
                    Physical_AddressLine3 = table.Column<string>(nullable: true),
                    Physical_PostalCode = table.Column<int>(nullable: false),
                    Physical_Province = table.Column<string>(nullable: true),
                    Posta_AddressLine1 = table.Column<string>(nullable: true),
                    Posta_AddressLine3 = table.Column<string>(nullable: true),
                    Posta_AddressLine4 = table.Column<string>(nullable: true),
                    Posta_PostalCode = table.Column<string>(nullable: true),
                    Posta_Province = table.Column<string>(nullable: true),
                    SchoolId = table.Column<int>(nullable: false),
                    Telephone = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolsAddress", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolsAddress");
        }
    }
}
