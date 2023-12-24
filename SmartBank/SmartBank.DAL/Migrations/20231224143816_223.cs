using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBank.DAL.Migrations
{
    /// <inheritdoc />
    public partial class _223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Money",
                table: "Expense",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money",
                table: "Expense");
        }
    }
}
