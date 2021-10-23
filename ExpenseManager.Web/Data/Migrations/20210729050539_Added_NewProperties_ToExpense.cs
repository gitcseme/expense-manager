using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManager.Web.Data.Migrations
{
    public partial class Added_NewProperties_ToExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Expenses",
                newName: "RicitImageName");

            migrationBuilder.AddColumn<string>(
                name: "ExpenseReferenceId",
                table: "Expenses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMode",
                table: "Expenses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentReference",
                table: "Expenses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseReferenceId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PaymentMode",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PaymentReference",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "RicitImageName",
                table: "Expenses",
                newName: "ImageName");
        }
    }
}
