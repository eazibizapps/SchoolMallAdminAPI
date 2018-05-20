using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApiJwt.Migrations
{
    public partial class menuestructure3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemSub_MenuItemSub_SubMenuItemId",
                table: "MenuItemSub");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemSub_SubMenuItemId",
                table: "MenuItemSub");

            migrationBuilder.DropColumn(
                name: "SubMenuItemId",
                table: "MenuItemSub");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemSub_MainMenuItemId",
                table: "MenuItemSub",
                column: "MainMenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemSub_MenuItemMain_MainMenuItemId",
                table: "MenuItemSub",
                column: "MainMenuItemId",
                principalTable: "MenuItemMain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemSub_MenuItemMain_MainMenuItemId",
                table: "MenuItemSub");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemSub_MainMenuItemId",
                table: "MenuItemSub");

            migrationBuilder.AddColumn<int>(
                name: "SubMenuItemId",
                table: "MenuItemSub",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemSub_SubMenuItemId",
                table: "MenuItemSub",
                column: "SubMenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemSub_MenuItemSub_SubMenuItemId",
                table: "MenuItemSub",
                column: "SubMenuItemId",
                principalTable: "MenuItemSub",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
