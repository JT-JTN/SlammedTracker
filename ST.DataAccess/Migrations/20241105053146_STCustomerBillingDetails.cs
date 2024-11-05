using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class STCustomerBillingDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessName",
                table: "Customers",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "navarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress2",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingCity",
                table: "Customers",
                type: "nvarchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingState",
                table: "Customers",
                type: "nvarchar(2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingZip",
                table: "Customers",
                type: "nvarchar(5)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingAddress2",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingCity",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingState",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingZip",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(50",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessName",
                table: "Customers",
                type: "navarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);
        }
    }
}
