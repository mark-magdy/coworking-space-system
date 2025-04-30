using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coworking_space.DAL.Migrations
{
    /// <inheritdoc />
    public partial class update_database_inOut_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerUpdate",
                table: "ReservationOfRooms",
                newName: "PriceTillNow");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedPriceDate",
                table: "ReservationOfRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "InOut",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedPriceDate",
                table: "ReservationOfRooms");

            migrationBuilder.DropColumn(
                name: "InOut",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PriceTillNow",
                table: "ReservationOfRooms",
                newName: "PricePerUpdate");
        }
    }
}
