using Microsoft.EntityFrameworkCore.Migrations;

namespace hakimslivs.Migrations
{
    public partial class orderstatusfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                schema: "Identity",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatus",
                schema: "Identity",
                table: "OrderStatus");

            migrationBuilder.RenameTable(
                name: "OrderStatus",
                schema: "Identity",
                newName: "OrdersStatuses",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersStatuses",
                schema: "Identity",
                table: "OrdersStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrdersStatuses_OrderStatusId",
                schema: "Identity",
                table: "Orders",
                column: "OrderStatusId",
                principalSchema: "Identity",
                principalTable: "OrdersStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrdersStatuses_OrderStatusId",
                schema: "Identity",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersStatuses",
                schema: "Identity",
                table: "OrdersStatuses");

            migrationBuilder.RenameTable(
                name: "OrdersStatuses",
                schema: "Identity",
                newName: "OrderStatus",
                newSchema: "Identity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatus",
                schema: "Identity",
                table: "OrderStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                schema: "Identity",
                table: "Orders",
                column: "OrderStatusId",
                principalSchema: "Identity",
                principalTable: "OrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
