using Microsoft.EntityFrameworkCore.Migrations;

namespace ClickApp.Data.Migrations
{
    public partial class AddedGeneralFieldToSkillAndInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interests_GeneralFields_GeneralFieldId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_GeneralFields_GeneralFieldId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_GeneralFields_GeneralFieldId",
                table: "UserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_GeneralFields_GeneralFieldId",
                table: "UserSkills");

            migrationBuilder.DropIndex(
                name: "IX_UserSkills_GeneralFieldId",
                table: "UserSkills");

            migrationBuilder.DropIndex(
                name: "IX_UserInterests_GeneralFieldId",
                table: "UserInterests");

            migrationBuilder.DropColumn(
                name: "GeneralFieldId",
                table: "UserSkills");

            migrationBuilder.DropColumn(
                name: "GeneralFieldId",
                table: "UserInterests");

            migrationBuilder.AlterColumn<int>(
                name: "GeneralFieldId",
                table: "Skills",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GeneralFieldId",
                table: "Interests",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_GeneralFields_GeneralFieldId",
                table: "Interests",
                column: "GeneralFieldId",
                principalTable: "GeneralFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_GeneralFields_GeneralFieldId",
                table: "Skills",
                column: "GeneralFieldId",
                principalTable: "GeneralFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interests_GeneralFields_GeneralFieldId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_GeneralFields_GeneralFieldId",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "GeneralFieldId",
                table: "UserSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GeneralFieldId",
                table: "UserInterests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "GeneralFieldId",
                table: "Skills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GeneralFieldId",
                table: "Interests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_GeneralFieldId",
                table: "UserSkills",
                column: "GeneralFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_GeneralFieldId",
                table: "UserInterests",
                column: "GeneralFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_GeneralFields_GeneralFieldId",
                table: "Interests",
                column: "GeneralFieldId",
                principalTable: "GeneralFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_GeneralFields_GeneralFieldId",
                table: "Skills",
                column: "GeneralFieldId",
                principalTable: "GeneralFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_GeneralFields_GeneralFieldId",
                table: "UserInterests",
                column: "GeneralFieldId",
                principalTable: "GeneralFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_GeneralFields_GeneralFieldId",
                table: "UserSkills",
                column: "GeneralFieldId",
                principalTable: "GeneralFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
