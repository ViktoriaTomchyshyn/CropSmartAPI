using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropSmartAPI.DAL.Migrations
{
    public partial class AddedSoilType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoilType",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoilType",
                table: "Fields");
        }
    }
}
