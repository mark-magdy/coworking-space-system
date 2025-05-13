using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coworking_space.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TotalReservationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TotalReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TotalReservations");
        }
    }
}
