using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class FixForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Statuses_StatusId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f15c6f7-b0eb-4d47-b306-f2bf7c0d068d",
                column: "ConcurrencyStamp",
                value: "bd32a8e0-cd35-4154-8f64-97b3ddf4eb29");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b33a46d-a182-4dc4-ac38-1fa2c917b9be",
                column: "ConcurrencyStamp",
                value: "04589def-495c-4243-9c65-70ea4ce6322c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4fafec6-f6ac-4b2d-aae7-bc4c20bb46bb",
                column: "ConcurrencyStamp",
                value: "45917879-72b7-4cd9-89bf-1e184054faa9");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Statuses_StatusId",
                table: "Tasks",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Statuses_StatusId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f15c6f7-b0eb-4d47-b306-f2bf7c0d068d",
                column: "ConcurrencyStamp",
                value: "6883234c-0d42-401e-9436-e5678438fc85");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b33a46d-a182-4dc4-ac38-1fa2c917b9be",
                column: "ConcurrencyStamp",
                value: "2184fa06-822d-448c-a6e1-6c975da389fb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4fafec6-f6ac-4b2d-aae7-bc4c20bb46bb",
                column: "ConcurrencyStamp",
                value: "3f299ce3-2f13-4488-a7e4-35bd219bcd30");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Statuses_StatusId",
                table: "Tasks",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
