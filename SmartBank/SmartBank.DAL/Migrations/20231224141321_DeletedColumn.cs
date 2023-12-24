using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBank.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeletedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTransfer",
                table: "Expense");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTransfer",
                table: "Expense",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
