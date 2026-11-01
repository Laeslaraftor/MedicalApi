using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalApi.Database.Migrations
{
    /// <inheritdoc />
    public partial class PatientIllessFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientIllnesses_Illnesses_DiseaseId",
                table: "PatientIllnesses");

            migrationBuilder.RenameColumn(
                name: "DiseaseId",
                table: "PatientIllnesses",
                newName: "IllnessId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientIllnesses_DiseaseId",
                table: "PatientIllnesses",
                newName: "IX_PatientIllnesses_IllnessId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientIllnesses_Illnesses_IllnessId",
                table: "PatientIllnesses",
                column: "IllnessId",
                principalTable: "Illnesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientIllnesses_Illnesses_IllnessId",
                table: "PatientIllnesses");

            migrationBuilder.RenameColumn(
                name: "IllnessId",
                table: "PatientIllnesses",
                newName: "DiseaseId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientIllnesses_IllnessId",
                table: "PatientIllnesses",
                newName: "IX_PatientIllnesses_DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientIllnesses_Illnesses_DiseaseId",
                table: "PatientIllnesses",
                column: "DiseaseId",
                principalTable: "Illnesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
