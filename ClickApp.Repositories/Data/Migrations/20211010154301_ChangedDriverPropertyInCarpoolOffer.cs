using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickApp.Data.Migrations
{
    public partial class ChangedDriverPropertyInCarpoolOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "CarpoolOffers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

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
    }
}
