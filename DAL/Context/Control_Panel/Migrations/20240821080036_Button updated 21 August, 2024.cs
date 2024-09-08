using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Context.Control_Panel.Migrations
{
    /// <inheritdoc />
    public partial class Buttonupdated21August2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUserButtons");

            migrationBuilder.AddColumn<long>(
                name: "ApplicationId",
                table: "tblButtons",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ButtonType",
                table: "tblButtons",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ModuleId",
                table: "tblButtons",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "tblButtons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblButtons_ApplicationId",
                table: "tblButtons",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblButtons_ModuleId",
                table: "tblButtons",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblButtons_UserId",
                table: "tblButtons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblButtons_AspNetUsers_UserId",
                table: "tblButtons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblButtons_tblApplications_ApplicationId",
                table: "tblButtons",
                column: "ApplicationId",
                principalTable: "tblApplications",
                principalColumn: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblButtons_tblModules_ModuleId",
                table: "tblButtons",
                column: "ModuleId",
                principalTable: "tblModules",
                principalColumn: "ModuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblButtons_AspNetUsers_UserId",
                table: "tblButtons");

            migrationBuilder.DropForeignKey(
                name: "FK_tblButtons_tblApplications_ApplicationId",
                table: "tblButtons");

            migrationBuilder.DropForeignKey(
                name: "FK_tblButtons_tblModules_ModuleId",
                table: "tblButtons");

            migrationBuilder.DropIndex(
                name: "IX_tblButtons_ApplicationId",
                table: "tblButtons");

            migrationBuilder.DropIndex(
                name: "IX_tblButtons_ModuleId",
                table: "tblButtons");

            migrationBuilder.DropIndex(
                name: "IX_tblButtons_UserId",
                table: "tblButtons");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "tblButtons");

            migrationBuilder.DropColumn(
                name: "ButtonType",
                table: "tblButtons");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "tblButtons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tblButtons");

            migrationBuilder.CreateTable(
                name: "tblUserButtons",
                columns: table => new
                {
                    UserButtonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ButtonId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActivatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelledBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeactivatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeactivatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeactivated = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    IsRejected = table.Column<bool>(type: "bit", nullable: false),
                    IsRestored = table.Column<bool>(type: "bit", nullable: false),
                    RejectedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RejectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RestoredBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RestoredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StateStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserButtons", x => x.UserButtonId);
                    table.ForeignKey(
                        name: "FK_tblUserButtons_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserButtons_tblButtons_ButtonId",
                        column: x => x.ButtonId,
                        principalTable: "tblButtons",
                        principalColumn: "ButtonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUserButtons_ButtonId",
                table: "tblUserButtons",
                column: "ButtonId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserButtons_UserId",
                table: "tblUserButtons",
                column: "UserId");
        }
    }
}
