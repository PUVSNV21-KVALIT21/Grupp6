using Microsoft.EntityFrameworkCore.Migrations;

namespace hakimslivs.Migrations
{
    public partial class icons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                schema: "Identity",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                schema: "Identity",
                table: "Categories");
        }
    }
}
