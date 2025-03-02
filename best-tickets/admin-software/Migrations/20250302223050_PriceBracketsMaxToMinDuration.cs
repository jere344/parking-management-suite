using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace admintickets.Migrations
{
    public partial class PriceBracketsMaxToMinDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InternalMaxDuration",
                table: "PriceBracket",
                newName: "InternalMinDuration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InternalMinDuration",
                table: "PriceBracket",
                newName: "InternalMaxDuration");
        }
    }
}
