using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coworking_space.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PriceTotalReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivateCapacity",
                table: "Rooms");

            

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "TotalReservations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "TotalReservations");

            migrationBuilder.AddColumn<int>(
                name: "PrivateCapacity",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPeople",
                table: "ReservationOfRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
