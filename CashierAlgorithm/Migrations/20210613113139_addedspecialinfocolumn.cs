using Microsoft.EntityFrameworkCore.Migrations;

namespace CashierAlgorithm.Migrations
{
    public partial class addedspecialinfocolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpecialInfo",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialInfo",
                table: "Workers");
        }
    }
}
