using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentService.Migrations
{
    public partial class ModifiedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "AuctionPayment");

            migrationBuilder.AddColumn<int>(
                name: "IdAuction",
                table: "AuctionPayment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdAuction",
                table: "AuctionPayment");

            migrationBuilder.AddColumn<int>(
                name: "AuctionId",
                table: "AuctionPayment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
