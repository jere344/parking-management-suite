using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace admintickets.Migrations
{
    public partial class Plural : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Taxes",
                table: "Taxes");

            migrationBuilder.RenameTable(
                name: "Taxes",
                newName: "Taxe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Taxe",
                table: "Taxe",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Taxe",
                table: "Taxe");

            migrationBuilder.RenameTable(
                name: "Taxe",
                newName: "Taxes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Taxes",
                table: "Taxes",
                column: "Id");
        }
    }
}
