using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vzeeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class v123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sepcializationId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sepcializationId",
                table: "Bookings");
        }
    }
}
