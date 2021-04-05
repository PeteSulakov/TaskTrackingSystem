using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedStatusesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f15c6f7-b0eb-4d47-b306-f2bf7c0d068d", "6883234c-0d42-401e-9436-e5678438fc85", "Developer", "DEVELOPER" },
                    { "8b33a46d-a182-4dc4-ac38-1fa2c917b9be", "2184fa06-822d-448c-a6e1-6c975da389fb", "Manager", "MANAGER" },
                    { "f4fafec6-f6ac-4b2d-aae7-bc4c20bb46bb", "3f299ce3-2f13-4488-a7e4-35bd219bcd30", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "Started" },
                    { 3, "Closed" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f15c6f7-b0eb-4d47-b306-f2bf7c0d068d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b33a46d-a182-4dc4-ac38-1fa2c917b9be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4fafec6-f6ac-4b2d-aae7-bc4c20bb46bb");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3);

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
    }
}
