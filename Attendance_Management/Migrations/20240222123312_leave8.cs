using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Management.Migrations
{
    /// <inheritdoc />
    public partial class leave8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_punches_leaves_LeaveId",
                table: "punches");

            migrationBuilder.DropIndex(
                name: "IX_punches_LeaveId",
                table: "punches");

            migrationBuilder.DropColumn(
                name: "LeaveId",
                table: "punches");

            migrationBuilder.AddColumn<int>(
                name: "Leave",
                table: "punches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Leave",
                table: "punches");

            migrationBuilder.AddColumn<int>(
                name: "LeaveId",
                table: "punches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_punches_LeaveId",
                table: "punches",
                column: "LeaveId");

            migrationBuilder.AddForeignKey(
                name: "FK_punches_leaves_LeaveId",
                table: "punches",
                column: "LeaveId",
                principalTable: "leaves",
                principalColumn: "Id");
        }
    }
}
