using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coworking_space.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeisavailableduetoerror : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Products",
                type: "bit",
                nullable: false,
                computedColumnSql: "CASE WHEN Quantity > 0 THEN 1 ELSE 0 END",
                stored: true);
        }
    }
}
