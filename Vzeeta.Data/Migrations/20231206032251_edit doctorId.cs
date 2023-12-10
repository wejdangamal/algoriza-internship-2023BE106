using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vzeeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class editdoctorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_doctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Appointments_AppointmentsID",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_AppointmentsID",
                table: "TimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_appUserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "AppointmentsID",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "appointmentId",
                table: "TimeSlots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "doctorId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "appUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_appointmentId",
                table: "TimeSlots",
                column: "appointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_doctorId",
                table: "Appointments",
                column: "doctorId",
                principalTable: "Doctors",
                principalColumn: "appUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Appointments_appointmentId",
                table: "TimeSlots",
                column: "appointmentId",
                principalTable: "Appointments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_doctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Appointments_appointmentId",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_appointmentId",
                table: "TimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "appointmentId",
                table: "TimeSlots");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentsID",
                table: "TimeSlots",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "doctorId",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_AppointmentsID",
                table: "TimeSlots",
                column: "AppointmentsID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_appUserId",
                table: "Doctors",
                column: "appUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_doctorId",
                table: "Appointments",
                column: "doctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Appointments_AppointmentsID",
                table: "TimeSlots",
                column: "AppointmentsID",
                principalTable: "Appointments",
                principalColumn: "ID");
        }
    }
}
