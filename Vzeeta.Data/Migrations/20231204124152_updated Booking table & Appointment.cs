using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vzeeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedBookingtableAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Doctors_DoctorId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TimeSlot_timeId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlot_DaySchedule_dayScheduleID",
                table: "TimeSlot");

            migrationBuilder.DropTable(
                name: "DaySchedule");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_DoctorId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSlot",
                table: "TimeSlot");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "TimeSlot",
                newName: "TimeSlots");

            migrationBuilder.RenameColumn(
                name: "dayScheduleID",
                table: "TimeSlots",
                newName: "AppointmentsID");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlot_dayScheduleID",
                table: "TimeSlots",
                newName: "IX_TimeSlots_AppointmentsID");

            migrationBuilder.AddColumn<decimal>(
                name: "finalPrice",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSlots",
                table: "TimeSlots",
                column: "timeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TimeSlots_timeId",
                table: "Bookings",
                column: "timeId",
                principalTable: "TimeSlots",
                principalColumn: "timeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Appointments_AppointmentsID",
                table: "TimeSlots",
                column: "AppointmentsID",
                principalTable: "Appointments",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TimeSlots_timeId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Appointments_AppointmentsID",
                table: "TimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSlots",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "finalPrice",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "TimeSlots",
                newName: "TimeSlot");

            migrationBuilder.RenameColumn(
                name: "AppointmentsID",
                table: "TimeSlot",
                newName: "dayScheduleID");

            migrationBuilder.RenameIndex(
                name: "IX_TimeSlots_AppointmentsID",
                table: "TimeSlot",
                newName: "IX_TimeSlot_dayScheduleID");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSlot",
                table: "TimeSlot",
                column: "timeId");

            migrationBuilder.CreateTable(
                name: "DaySchedule",
                columns: table => new
                {
                    dayScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentsID = table.Column<int>(type: "int", nullable: true),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySchedule", x => x.dayScheduleID);
                    table.ForeignKey(
                        name: "FK_DaySchedule_Appointments_AppointmentsID",
                        column: x => x.AppointmentsID,
                        principalTable: "Appointments",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DoctorId",
                table: "Bookings",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DaySchedule_AppointmentsID",
                table: "DaySchedule",
                column: "AppointmentsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Doctors_DoctorId",
                table: "Bookings",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TimeSlot_timeId",
                table: "Bookings",
                column: "timeId",
                principalTable: "TimeSlot",
                principalColumn: "timeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlot_DaySchedule_dayScheduleID",
                table: "TimeSlot",
                column: "dayScheduleID",
                principalTable: "DaySchedule",
                principalColumn: "dayScheduleID");
        }
    }
}
