using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class menuestructure1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubMenuItem_SubMenuItem_SubMenuItemId",
                table: "SubMenuItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubMenuItem",
                table: "SubMenuItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainMenuItem",
                table: "MainMenuItem");

            migrationBuilder.RenameTable(
                name: "SubMenuItem",
                newName: "MenuItemSub");

            migrationBuilder.RenameTable(
                name: "MainMenuItem",
                newName: "MenuItemMain");

            migrationBuilder.RenameIndex(
                name: "IX_SubMenuItem_SubMenuItemId",
                table: "MenuItemSub",
                newName: "IX_MenuItemSub_SubMenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemSub",
                table: "MenuItemSub",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemMain",
                table: "MenuItemMain",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemSub_MenuItemSub_SubMenuItemId",
                table: "MenuItemSub",
                column: "SubMenuItemId",
                principalTable: "MenuItemSub",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemSub_MenuItemSub_SubMenuItemId",
                table: "MenuItemSub");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemSub",
                table: "MenuItemSub");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemMain",
                table: "MenuItemMain");

            migrationBuilder.RenameTable(
                name: "MenuItemSub",
                newName: "SubMenuItem");

            migrationBuilder.RenameTable(
                name: "MenuItemMain",
                newName: "MainMenuItem");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemSub_SubMenuItemId",
                table: "SubMenuItem",
                newName: "IX_SubMenuItem_SubMenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubMenuItem",
                table: "SubMenuItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainMenuItem",
                table: "MainMenuItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubMenuItem_SubMenuItem_SubMenuItemId",
                table: "SubMenuItem",
                column: "SubMenuItemId",
                principalTable: "SubMenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
