using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickApp.Data.Migrations
{
    public partial class ChangedNameOfPropertyPrivate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Private",
                table: "JournalEntrys");

            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "JournalEntrys",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Public",
                table: "JournalEntrys");

            migrationBuilder.AddColumn<bool>(
                name: "Private",
                table: "JournalEntrys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
