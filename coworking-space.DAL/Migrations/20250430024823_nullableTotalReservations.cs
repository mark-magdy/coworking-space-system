using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coworking_space.DAL.Migrations
{
    /// <inheritdoc />
    public partial class nullableTotalReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TotalReservations_Users_userId",
                table: "TotalReservations");

            migrationBuilder.DropColumn(
                name: "UsrId",
                table: "TotalReservations");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "TotalReservations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TotalReservations_userId",
                table: "TotalReservations",
                newName: "IX_TotalReservations_UserId");

           

            // Add the new column PriceTillNow
           
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TotalReservations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PrivateCapacity",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

           
            

            migrationBuilder.AddForeignKey(
                name: "FK_TotalReservations_Users_UserId",
                table: "TotalReservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TotalReservations_Users_UserId",
                table: "TotalReservations");

            migrationBuilder.DropColumn(
                name: "PrivateCapacity",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "UpdatedPriceDate",
                table: "ReservationOfRooms");

            migrationBuilder.DropColumn(
                name: "InOut",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TotalReservations",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_TotalReservations_UserId",
                table: "TotalReservations",
                newName: "IX_TotalReservations_userId");

            migrationBuilder.RenameColumn(
                name: "PriceTillNow",
                table: "ReservationOfRooms",
                newName: "PricePerUpdate");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TotalReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsrId",
                table: "TotalReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TotalReservations_Users_userId",
                table: "TotalReservations",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
