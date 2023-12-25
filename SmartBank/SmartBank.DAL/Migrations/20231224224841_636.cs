using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBank.DAL.Migrations
{
    /// <inheritdoc />
    public partial class _636 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpendedMoneyAmount",
                table: "Report");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SpendedMoneyAmount",
                table: "Report",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
