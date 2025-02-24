using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace admintickets.Migrations
{
    public partial class PaymentCanBeNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TicketPayment_TicketPaymentId",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "TicketPaymentId",
                table: "Ticket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_TicketPayment_TicketPaymentId",
                table: "Ticket",
                column: "TicketPaymentId",
                principalTable: "TicketPayment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TicketPayment_TicketPaymentId",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "TicketPaymentId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_TicketPayment_TicketPaymentId",
                table: "Ticket",
                column: "TicketPaymentId",
                principalTable: "TicketPayment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
