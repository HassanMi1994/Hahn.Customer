using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hahn.Customers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removephonenumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "TEXT",
                nullable: true);
        }
    }
}
