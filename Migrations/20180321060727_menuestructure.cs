using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class menuestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainMenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(nullable: true),
                    Fragment = table.Column<string>(nullable: true),
                    Group = table.Column<bool>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false),
                    Home = table.Column<bool>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    PathMatch = table.Column<string>(nullable: true),
                    Selected = table.Column<bool>(nullable: false),
                    SubMenuHeight = table.Column<int>(nullable: false),
                    Target = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    expanded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainMenuItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubMenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(nullable: true),
                    Fragment = table.Column<string>(nullable: true),
                    Group = table.Column<bool>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false),
                    Home = table.Column<bool>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    MainMenuItemId = table.Column<int>(nullable: false),
                    PathMatch = table.Column<string>(nullable: true),
                    Selected = table.Column<bool>(nullable: false),
                    SubMenuHeight = table.Column<int>(nullable: false),
                    SubMenuItemId = table.Column<int>(nullable: true),
                    Target = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    expanded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubMenuItem_SubMenuItem_SubMenuItemId",
                        column: x => x.SubMenuItemId,
                        principalTable: "SubMenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubMenuItem_SubMenuItemId",
                table: "SubMenuItem",
                column: "SubMenuItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainMenuItem");

            migrationBuilder.DropTable(
                name: "SubMenuItem");
        }
    }
}
