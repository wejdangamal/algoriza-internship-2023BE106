using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vzeeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditPatientCodetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiscountCodes_DiscountCodeCoupons_CodeID",
                table: "PatientDiscountCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientDiscountCodes",
                table: "PatientDiscountCodes");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiscountCodes_CodeID",
                table: "PatientDiscountCodes");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiscountCodes_patientID",
                table: "PatientDiscountCodes");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "PatientDiscountCodes");

            migrationBuilder.DropColumn(
                name: "CodeID",
                table: "PatientDiscountCodes");

            migrationBuilder.AlterColumn<string>(
                name: "patientID",
                table: "PatientDiscountCodes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "dicountCodeID",
                table: "PatientDiscountCodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientDiscountCodes",
                table: "PatientDiscountCodes",
                columns: new[] { "patientID", "dicountCodeID" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiscountCodes_dicountCodeID",
                table: "PatientDiscountCodes",
                column: "dicountCodeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiscountCodes_DiscountCodeCoupons_dicountCodeID",
                table: "PatientDiscountCodes",
                column: "dicountCodeID",
                principalTable: "DiscountCodeCoupons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiscountCodes_DiscountCodeCoupons_dicountCodeID",
                table: "PatientDiscountCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientDiscountCodes",
                table: "PatientDiscountCodes");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiscountCodes_dicountCodeID",
                table: "PatientDiscountCodes");

            migrationBuilder.AlterColumn<int>(
                name: "dicountCodeID",
                table: "PatientDiscountCodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "patientID",
                table: "PatientDiscountCodes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "PatientDiscountCodes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CodeID",
                table: "PatientDiscountCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientDiscountCodes",
                table: "PatientDiscountCodes",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiscountCodes_CodeID",
                table: "PatientDiscountCodes",
                column: "CodeID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiscountCodes_patientID",
                table: "PatientDiscountCodes",
                column: "patientID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiscountCodes_DiscountCodeCoupons_CodeID",
                table: "PatientDiscountCodes",
                column: "CodeID",
                principalTable: "DiscountCodeCoupons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
