using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a2e1ddb6-904e-4eab-92b2-0b28956f1e36", "00dcced5-7ad1-49bc-8605-1a9d580fbd89", "Developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cf0bc83e-9112-434b-a5a8-76a7cbbe72af", "530977db-2f42-4433-914f-678ed3873573", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dcf39e56-19a8-4cab-af44-59233bd6e309", "44957591-9d79-4334-9140-53aed3702afc", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2e1ddb6-904e-4eab-92b2-0b28956f1e36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf0bc83e-9112-434b-a5a8-76a7cbbe72af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dcf39e56-19a8-4cab-af44-59233bd6e309");
        }
    }
}
