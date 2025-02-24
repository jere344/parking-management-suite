using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace admintickets.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodes_Hospitals_HospitalId",
                table: "DiscountCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionTokens_Users_UserId",
                table: "SessionTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Signal_Hospitals_HospitalId",
                table: "Signal");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionTiers_Hospitals_HospitalId",
                table: "SubscriptionTiers");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketPayment_DiscountCodes_CodeUsedId",
                table: "TicketPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Hospitals_HospitalId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketPayment_TicketPaymentId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionTiers",
                table: "SubscriptionTiers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionTokens",
                table: "SessionTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hospitals",
                table: "Hospitals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountCodes",
                table: "DiscountCodes");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameTable(
                name: "SubscriptionTiers",
                newName: "SubscriptionTier");

            migrationBuilder.RenameTable(
                name: "SessionTokens",
                newName: "SessionToken");

            migrationBuilder.RenameTable(
                name: "Hospitals",
                newName: "Hospital");

            migrationBuilder.RenameTable(
                name: "DiscountCodes",
                newName: "DiscountCode");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_TicketPaymentId",
                table: "Ticket",
                newName: "IX_Ticket_TicketPaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_HospitalId",
                table: "Ticket",
                newName: "IX_Ticket_HospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionTiers_HospitalId",
                table: "SubscriptionTier",
                newName: "IX_SubscriptionTier_HospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionTokens_UserId",
                table: "SessionToken",
                newName: "IX_SessionToken_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountCodes_HospitalId",
                table: "DiscountCode",
                newName: "IX_DiscountCode_HospitalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionTier",
                table: "SubscriptionTier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionToken",
                table: "SessionToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hospital",
                table: "Hospital",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountCode",
                table: "DiscountCode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCode_Hospital_HospitalId",
                table: "DiscountCode",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionToken_User_UserId",
                table: "SessionToken",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Signal_Hospital_HospitalId",
                table: "Signal",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionTier_Hospital_HospitalId",
                table: "SubscriptionTier",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Hospital_HospitalId",
                table: "Ticket",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_TicketPayment_TicketPaymentId",
                table: "Ticket",
                column: "TicketPaymentId",
                principalTable: "TicketPayment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPayment_DiscountCode_CodeUsedId",
                table: "TicketPayment",
                column: "CodeUsedId",
                principalTable: "DiscountCode",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCode_Hospital_HospitalId",
                table: "DiscountCode");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionToken_User_UserId",
                table: "SessionToken");

            migrationBuilder.DropForeignKey(
                name: "FK_Signal_Hospital_HospitalId",
                table: "Signal");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionTier_Hospital_HospitalId",
                table: "SubscriptionTier");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Hospital_HospitalId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TicketPayment_TicketPaymentId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketPayment_DiscountCode_CodeUsedId",
                table: "TicketPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionTier",
                table: "SubscriptionTier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionToken",
                table: "SessionToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hospital",
                table: "Hospital");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscountCode",
                table: "DiscountCode");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "SubscriptionTier",
                newName: "SubscriptionTiers");

            migrationBuilder.RenameTable(
                name: "SessionToken",
                newName: "SessionTokens");

            migrationBuilder.RenameTable(
                name: "Hospital",
                newName: "Hospitals");

            migrationBuilder.RenameTable(
                name: "DiscountCode",
                newName: "DiscountCodes");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_TicketPaymentId",
                table: "Tickets",
                newName: "IX_Tickets_TicketPaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_HospitalId",
                table: "Tickets",
                newName: "IX_Tickets_HospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionTier_HospitalId",
                table: "SubscriptionTiers",
                newName: "IX_SubscriptionTiers_HospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionToken_UserId",
                table: "SessionTokens",
                newName: "IX_SessionTokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountCode_HospitalId",
                table: "DiscountCodes",
                newName: "IX_DiscountCodes_HospitalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionTiers",
                table: "SubscriptionTiers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionTokens",
                table: "SessionTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hospitals",
                table: "Hospitals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscountCodes",
                table: "DiscountCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodes_Hospitals_HospitalId",
                table: "DiscountCodes",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionTokens_Users_UserId",
                table: "SessionTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Signal_Hospitals_HospitalId",
                table: "Signal",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionTiers_Hospitals_HospitalId",
                table: "SubscriptionTiers",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPayment_DiscountCodes_CodeUsedId",
                table: "TicketPayment",
                column: "CodeUsedId",
                principalTable: "DiscountCodes",
                principalColumn: "Id");

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
    }
}
