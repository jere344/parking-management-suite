using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace admintickets.Migrations
{
    public partial class Ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Hospitals_HospitalId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TicketPayment_TicketPaymentId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_TicketPaymentId",
                table: "Tickets",
                newName: "IX_Tickets_TicketPaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_HospitalId",
                table: "Tickets",
                newName: "IX_Tickets_HospitalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Hospitals_HospitalId",
                table: "Tickets",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketPayment_TicketPaymentId",
                table: "Tickets",
                column: "TicketPaymentId",
                principalTable: "TicketPayment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Hospitals_HospitalId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketPayment_TicketPaymentId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_TicketPaymentId",
                table: "Ticket",
                newName: "IX_Ticket_TicketPaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_HospitalId",
                table: "Ticket",
                newName: "IX_Ticket_HospitalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Hospitals_HospitalId",
                table: "Ticket",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
