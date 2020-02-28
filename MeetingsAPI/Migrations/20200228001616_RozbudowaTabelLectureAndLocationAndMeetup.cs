using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingsAPI.Migrations
{
    public partial class RozbudowaTabelLectureAndLocationAndMeetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Meetups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Lectures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Meetups");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Lectures");
        }
    }
}
