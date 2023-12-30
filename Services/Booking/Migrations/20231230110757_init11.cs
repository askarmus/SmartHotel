using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHotel.BookingService.Migrations
{
    /// <inheritdoc />
    public partial class init11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookingStatus",
                table: "Bookings",
                newName: "PaymentStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Bookings",
                newName: "BookingStatus");
        }
    }
}
