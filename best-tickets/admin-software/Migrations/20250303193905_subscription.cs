using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace admintickets.Migrations
{
    public partial class subscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Subscription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_HospitalId",
                table: "Subscription",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Hospital_HospitalId",
                table: "Subscription",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Hospital_HospitalId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_HospitalId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Subscription");
        }
    }
}
