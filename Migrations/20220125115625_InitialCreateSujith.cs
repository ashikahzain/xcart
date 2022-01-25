using Microsoft.EntityFrameworkCore.Migrations;

namespace xcart.Migrations
{
    public partial class InitialCreateSujith : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AwardHistory_User_UserId",
                table: "AwardHistory");

            migrationBuilder.DropIndex(
                name: "IX_AwardHistory_UserId",
                table: "AwardHistory");

            migrationBuilder.DropColumn(
                name: "UsedId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AwardHistory");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "AwardHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardHistory_EmployeeId",
                table: "AwardHistory",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AwardHistory_User_EmployeeId",
                table: "AwardHistory",
                column: "EmployeeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_User_UserId",
                table: "Cart",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AwardHistory_User_EmployeeId",
                table: "AwardHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_User_UserId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UserId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_AwardHistory_EmployeeId",
                table: "AwardHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AwardHistory");

            migrationBuilder.AddColumn<long>(
                name: "UsedId",
                table: "Cart",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AwardHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwardHistory_UserId",
                table: "AwardHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AwardHistory_User_UserId",
                table: "AwardHistory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
