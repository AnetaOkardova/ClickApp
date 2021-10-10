using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickApp.Data.Migrations
{
    public partial class AddedCarpoolOfferAndRequestAndAcceptanceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarpoolOffers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<string>(nullable: false),
                    LeavingFrom = table.Column<string>(nullable: false),
                    ArrivingAt = table.Column<string>(nullable: false),
                    LeavingHour = table.Column<int>(nullable: false),
                    LeavingMinutes = table.Column<int>(nullable: false),
                    SeatsAvailable = table.Column<int>(nullable: false),
                    LeavingNote = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ReturnFrom = table.Column<string>(nullable: true),
                    ReturnAt = table.Column<string>(nullable: true),
                    ReturnHour = table.Column<int>(nullable: true),
                    ReturnMinutes = table.Column<int>(nullable: true),
                    ReturnSeatsAvailable = table.Column<int>(nullable: true),
                    ReturnNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarpoolOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarpoolOffers_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarpoolPassengerAcceptances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarpoolOfferId = table.Column<int>(nullable: false),
                    AcceptedPassengerId = table.Column<string>(nullable: true),
                    ReservedSeats = table.Column<int>(nullable: false),
                    DateAccepted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarpoolPassengerAcceptances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarpoolPassengerAcceptances_CarpoolOffers_CarpoolOfferId",
                        column: x => x.CarpoolOfferId,
                        principalTable: "CarpoolOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarpoolPassengerRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarpoolOfferId = table.Column<int>(nullable: false),
                    RequestingPassengerId = table.Column<string>(nullable: true),
                    RequestedSeats = table.Column<int>(nullable: false),
                    DateRequested = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarpoolPassengerRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarpoolPassengerRequests_CarpoolOffers_CarpoolOfferId",
                        column: x => x.CarpoolOfferId,
                        principalTable: "CarpoolOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarpoolOffers_DriverId",
                table: "CarpoolOffers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_CarpoolPassengerAcceptances_CarpoolOfferId",
                table: "CarpoolPassengerAcceptances",
                column: "CarpoolOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_CarpoolPassengerRequests_CarpoolOfferId",
                table: "CarpoolPassengerRequests",
                column: "CarpoolOfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarpoolPassengerAcceptances");

            migrationBuilder.DropTable(
                name: "CarpoolPassengerRequests");

            migrationBuilder.DropTable(
                name: "CarpoolOffers");
        }
    }
}
