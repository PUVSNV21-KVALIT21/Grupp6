using Microsoft.EntityFrameworkCore.Migrations;

namespace hakimslivs.Migrations
{
    public partial class orderstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                schema: "Identity",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PaymentOk",
                schema: "Identity",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                schema: "Identity",
                table: "Orders",
                column: "OrderStatusId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                schema: "Identity",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderStatus",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatusId",
                schema: "Identity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                schema: "Identity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentOk",
                schema: "Identity",
                table: "Orders");
        }
    }
}
