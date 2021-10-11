using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickApp.Data.Migrations
{
    public partial class ChangedCarpoolPassengerRequestAndAcceptanceModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptedStatus",
                table: "CarpoolPassengerRequests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "CarpoolPassengerRequests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptedStatus",
                table: "CarpoolPassengerAcceptances",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "CarpoolPassengerAcceptances",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedStatus",
                table: "CarpoolPassengerRequests");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "CarpoolPassengerRequests");

            migrationBuilder.DropColumn(
                name: "AcceptedStatus",
                table: "CarpoolPassengerAcceptances");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "CarpoolPassengerAcceptances");
        }
    }
}
