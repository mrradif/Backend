using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Context.Control_Panel.Migrations
{
    /// <inheritdoc />
    public partial class Buttonupdated221August2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApplicationId",
                table: "tblComponents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModuleId",
                table: "tblComponents",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblComponents_ApplicationId",
                table: "tblComponents",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblComponents_ModuleId",
                table: "tblComponents",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblComponents_tblApplications_ApplicationId",
                table: "tblComponents",
                column: "ApplicationId",
                principalTable: "tblApplications",
                principalColumn: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblComponents_tblModules_ModuleId",
                table: "tblComponents",
                column: "ModuleId",
                principalTable: "tblModules",
                principalColumn: "ModuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblComponents_tblApplications_ApplicationId",
                table: "tblComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_tblComponents_tblModules_ModuleId",
                table: "tblComponents");

            migrationBuilder.DropIndex(
                name: "IX_tblComponents_ApplicationId",
                table: "tblComponents");

            migrationBuilder.DropIndex(
                name: "IX_tblComponents_ModuleId",
                table: "tblComponents");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tblComponents");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "tblComponents");
        }
    }
}
