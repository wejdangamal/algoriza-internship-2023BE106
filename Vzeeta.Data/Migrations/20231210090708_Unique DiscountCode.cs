using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vzeeta.Data.Migrations
{
    /// <inheritdoc />
    public partial class UniqueDiscountCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientDiscountCodes");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DiscountCodeCoupons",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodeCoupons_Code",
                table: "DiscountCodeCoupons",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DiscountCodeCoupons_Code",
                table: "DiscountCodeCoupons");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "DiscountCodeCoupons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "PatientDiscountCodes",
                columns: table => new
                {
                    dicountCodeID = table.Column<int>(type: "int", nullable: false),
                    patientID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsedCode = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDiscountCodes", x => new { x.patientID, x.dicountCodeID });
                    table.ForeignKey(
                        name: "FK_PatientDiscountCodes_AspNetUsers_patientID",
                        column: x => x.patientID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientDiscountCodes_DiscountCodeCoupons_dicountCodeID",
                        column: x => x.dicountCodeID,
                        principalTable: "DiscountCodeCoupons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiscountCodes_dicountCodeID",
                table: "PatientDiscountCodes",
                column: "dicountCodeID");
        }
    }
}
