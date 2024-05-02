using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Management.Migrations
{
    /// <inheritdoc />
    public partial class leave9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Leave",
                table: "punches");

            migrationBuilder.AddColumn<int>(
                name: "AttandanceStatusl",
                table: "leaves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttandanceStatusl",
                table: "leaves");

            migrationBuilder.AddColumn<int>(
                name: "Leave",
                table: "punches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
