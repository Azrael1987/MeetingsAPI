using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingsAPI.Migrations
{
    public partial class AddSpecialGuestsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecialGuests",
                columns: table => new
                {
                    SpecialGuestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Citzenship = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialGuests", x => x.SpecialGuestId);
                });

            migrationBuilder.CreateTable(
                name: "SpecialGuestJoint",
                columns: table => new
                {
                    MeetupId = table.Column<int>(nullable: false),
                    FirstSpecialGuestId = table.Column<int>(nullable: false),
                    SecondSpecialGuestId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialGuestJoint", x => new { x.MeetupId, x.FirstSpecialGuestId, x.SecondSpecialGuestId });
                    table.UniqueConstraint("AK_SpecialGuestJoint_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialGuestJoint_SpecialGuests_FirstSpecialGuestId",
                        column: x => x.FirstSpecialGuestId,
                        principalTable: "SpecialGuests",
                        principalColumn: "SpecialGuestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecialGuestJoint_Meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialGuestJoint_SpecialGuests_SecondSpecialGuestId",
                        column: x => x.SecondSpecialGuestId,
                        principalTable: "SpecialGuests",
                        principalColumn: "SpecialGuestId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialGuestJoint_FirstSpecialGuestId",
                table: "SpecialGuestJoint",
                column: "FirstSpecialGuestId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialGuestJoint_SecondSpecialGuestId",
                table: "SpecialGuestJoint",
                column: "SecondSpecialGuestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialGuestJoint");

            migrationBuilder.DropTable(
                name: "SpecialGuests");
        }
    }
}
