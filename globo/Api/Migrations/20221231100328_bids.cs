using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class bids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BidEntity_Houses_HouseId",
                table: "BidEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BidEntity",
                table: "BidEntity");

            migrationBuilder.RenameTable(
                name: "BidEntity",
                newName: "Bids");

            migrationBuilder.RenameIndex(
                name: "IX_BidEntity_HouseId",
                table: "Bids",
                newName: "IX_Bids_HouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Houses_HouseId",
                table: "Bids",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Houses_HouseId",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.RenameTable(
                name: "Bids",
                newName: "BidEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_HouseId",
                table: "BidEntity",
                newName: "IX_BidEntity_HouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BidEntity",
                table: "BidEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BidEntity_Houses_HouseId",
                table: "BidEntity",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
