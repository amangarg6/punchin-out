using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Management.Migrations
{
    /// <inheritdoc />
    public partial class Leave1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "leaves",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_leaves_ApplicationUserId",
                table: "leaves",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_leaves_AspNetUsers_ApplicationUserId",
                table: "leaves",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaves_AspNetUsers_ApplicationUserId",
                table: "leaves");

            migrationBuilder.DropIndex(
                name: "IX_leaves_ApplicationUserId",
                table: "leaves");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "leaves");
        }
    }
}
