using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickApp.Data.Migrations
{
    public partial class AddedUserToCarpoolOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "CarpoolOffers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CarpoolOffers_DriverId",
                table: "CarpoolOffers",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarpoolOffers_AspNetUsers_DriverId",
                table: "CarpoolOffers",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarpoolOffers_AspNetUsers_DriverId",
                table: "CarpoolOffers");

            migrationBuilder.DropIndex(
                name: "IX_CarpoolOffers_DriverId",
                table: "CarpoolOffers");

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "CarpoolOffers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
