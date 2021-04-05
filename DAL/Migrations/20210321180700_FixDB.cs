using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class FixDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ManagerId1",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_DeveloperId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_DeveloperId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ManagerId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeveloperId1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ManagerId1",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "DeveloperId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_DeveloperId",
                table: "Tasks",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ManagerId",
                table: "Projects",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_DeveloperId",
                table: "Tasks",
                column: "DeveloperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ManagerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_DeveloperId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_DeveloperId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "DeveloperId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeveloperId1",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId1",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_DeveloperId1",
                table: "Tasks",
                column: "DeveloperId1");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId1",
                table: "Projects",
                column: "ManagerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ManagerId1",
                table: "Projects",
                column: "ManagerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_DeveloperId1",
                table: "Tasks",
                column: "DeveloperId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
