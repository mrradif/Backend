using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Context.Control_Panel.Migrations
{
    /// <inheritdoc />
    public partial class ButtonAdded21August2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "tblButtons");

            migrationBuilder.DropColumn(
                name: "OnClickActionName",
                table: "tblButtons");

            migrationBuilder.RenameColumn(
                name: "IsVisible",
                table: "tblButtons",
                newName: "IsDisabled");

            migrationBuilder.CreateTable(
                name: "UserButtons",
                columns: table => new
                {
                    UserButtonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ButtonId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StateStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRestored = table.Column<bool>(type: "bit", nullable: false),
                    RestoredBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RestoredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    ActivatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActivatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeactivated = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeactivatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRejected = table.Column<bool>(type: "bit", nullable: false),
                    RejectedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RejectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    CancelledBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserButtons", x => x.UserButtonId);
                    table.ForeignKey(
                        name: "FK_UserButtons_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserButtons_tblButtons_ButtonId",
                        column: x => x.ButtonId,
                        principalTable: "tblButtons",
                        principalColumn: "ButtonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserButtons_ButtonId",
                table: "UserButtons",
                column: "ButtonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserButtons_UserId",
                table: "UserButtons",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserButtons");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "tblButtons",
                newName: "IsVisible");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "tblButtons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OnClickActionName",
                table: "tblButtons",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
