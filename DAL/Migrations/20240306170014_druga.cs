using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class druga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_Orders_OrderId",
                table: "OrderPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_GroupId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserGroups_GroupId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "BasketPositions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "BasketPositions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketPositions_ProductId1",
                table: "BasketPositions",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_BasketPositions_UserId1",
                table: "BasketPositions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketPositions_Products_ProductId1",
                table: "BasketPositions",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketPositions_Users_UserId1",
                table: "BasketPositions",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_Orders_OrderId",
                table: "OrderPositions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_GroupId",
                table: "Products",
                column: "GroupId",
                principalTable: "ProductGroups",
                principalColumn: "ProductGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserGroups_GroupId",
                table: "Users",
                column: "GroupId",
                principalTable: "UserGroups",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketPositions_Products_ProductId1",
                table: "BasketPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketPositions_Users_UserId1",
                table: "BasketPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositions_Orders_OrderId",
                table: "OrderPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_GroupId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserGroups_GroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_BasketPositions_ProductId1",
                table: "BasketPositions");

            migrationBuilder.DropIndex(
                name: "IX_BasketPositions_UserId1",
                table: "BasketPositions");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "BasketPositions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BasketPositions");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPositions_Orders_OrderId",
                table: "OrderPositions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_GroupId",
                table: "Products",
                column: "GroupId",
                principalTable: "ProductGroups",
                principalColumn: "ProductGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserGroups_GroupId",
                table: "Users",
                column: "GroupId",
                principalTable: "UserGroups",
                principalColumn: "UserGroupId");
        }
    }
}
