using Microsoft.EntityFrameworkCore.Migrations;

namespace hakimslivs.Migrations
{
    public partial class addedCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "Identity",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                schema: "Identity",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Identity",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryName",
                schema: "Identity",
                table: "Items",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "Identity",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryName",
                schema: "Identity",
                table: "Items",
                column: "CategoryName",
                principalSchema: "Identity",
                principalTable: "Categories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryName",
                schema: "Identity",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Items_CategoryName",
                schema: "Identity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                schema: "Identity",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "Identity",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
