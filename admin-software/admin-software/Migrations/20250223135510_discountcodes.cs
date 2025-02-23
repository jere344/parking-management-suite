using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace admintickets.Migrations
{
    public partial class discountcodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketPayment_Code_CodeUsedId",
                table: "TicketPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Code",
                table: "Code");

            migrationBuilder.RenameTable(
                name: "Code",
                newName: "DiscountCodes");

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "DiscountCodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountCodes",
                table: "DiscountCodes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_HospitalId",
                table: "DiscountCodes",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodes_Hospitals_HospitalId",
                table: "DiscountCodes",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPayment_DiscountCodes_CodeUsedId",
                table: "TicketPayment",
                column: "CodeUsedId",
                principalTable: "DiscountCodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodes_Hospitals_HospitalId",
                table: "DiscountCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketPayment_DiscountCodes_CodeUsedId",
                table: "TicketPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountCodes",
                table: "DiscountCodes");

            migrationBuilder.DropIndex(
                name: "IX_DiscountCodes_HospitalId",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "DiscountCodes");

            migrationBuilder.RenameTable(
                name: "DiscountCodes",
                newName: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Code",
                table: "Code",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPayment_Code_CodeUsedId",
                table: "TicketPayment",
                column: "CodeUsedId",
                principalTable: "Code",
                principalColumn: "Id");
        }
    }
}
