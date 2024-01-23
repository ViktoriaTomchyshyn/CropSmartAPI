using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropSmartAPI.DAL.Migrations
{
    public partial class AddedCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CoordinateX",
                table: "Fields",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CoordinateY",
                table: "Fields",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoordinateX",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CoordinateY",
                table: "Fields");
        }
    }
}
