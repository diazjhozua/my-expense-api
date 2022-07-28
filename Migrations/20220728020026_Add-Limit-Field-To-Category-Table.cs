using Microsoft.EntityFrameworkCore.Migrations;

namespace my_expense_api.Migrations
{
    public partial class AddLimitFieldToCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Limit",
                table: "Categories",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Limit",
                table: "Categories");
        }
    }
}
