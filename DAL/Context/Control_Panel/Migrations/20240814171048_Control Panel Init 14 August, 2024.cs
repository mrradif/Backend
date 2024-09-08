using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Context.Control_Panel.Migrations
{
    /// <inheritdoc />
    public partial class ControlPanelInit14August2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiresTwoFactorAuthentication",
                table: "tblOrganizationPolicyConfigs",
                newName: "RequireTwoFactorAuthentication");

            migrationBuilder.AddColumn<string>(
                name: "ActivatedBy",
                table: "tblOrganizationPolicyConfigs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivatedDate",
                table: "tblOrganizationPolicyConfigs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "tblOrganizationPolicyConfigs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "tblOrganizationPolicyConfigs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelledBy",
                table: "tblOrganizationPolicyConfigs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledDate",
                table: "tblOrganizationPolicyConfigs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeactivatedBy",
                table: "tblOrganizationPolicyConfigs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivatedDate",
                table: "tblOrganizationPolicyConfigs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "tblOrganizationPolicyConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "tblOrganizationPolicyConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "tblOrganizationPolicyConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "tblOrganizationPolicyConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "tblOrganizationPolicyConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRestored",
                table: "tblOrganizationPolicyConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RejectedBy",
                table: "tblOrganizationPolicyConfigs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedDate",
                table: "tblOrganizationPolicyConfigs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RestoredBy",
                table: "tblOrganizationPolicyConfigs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RestoredDate",
                table: "tblOrganizationPolicyConfigs",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivatedBy",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "ActivatedDate",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "CancelledBy",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "CancelledDate",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "DeactivatedBy",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "DeactivatedDate",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "IsRestored",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "RejectedBy",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "RejectedDate",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "RestoredBy",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "RestoredDate",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.RenameColumn(
                name: "RequireTwoFactorAuthentication",
                table: "tblOrganizationPolicyConfigs",
                newName: "RequiresTwoFactorAuthentication");
        }
    }
}
