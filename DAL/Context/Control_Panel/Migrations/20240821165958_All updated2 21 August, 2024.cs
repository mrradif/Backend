using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Context.Control_Panel.Migrations
{
    /// <inheritdoc />
    public partial class Allupdated221August2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivatedBy",
                table: "tblModules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivatedDate",
                table: "tblModules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "tblModules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "tblModules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelledBy",
                table: "tblModules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledDate",
                table: "tblModules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeactivatedBy",
                table: "tblModules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivatedDate",
                table: "tblModules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "tblModules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "tblModules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "tblModules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "tblModules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "tblModules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRestored",
                table: "tblModules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RejectedBy",
                table: "tblModules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedDate",
                table: "tblModules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RestoredBy",
                table: "tblModules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RestoredDate",
                table: "tblModules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActivatedBy",
                table: "tblApplications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivatedDate",
                table: "tblApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "tblApplications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "tblApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelledBy",
                table: "tblApplications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledDate",
                table: "tblApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeactivatedBy",
                table: "tblApplications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivatedDate",
                table: "tblApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "tblApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "tblApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "tblApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                table: "tblApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "tblApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRestored",
                table: "tblApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RejectedBy",
                table: "tblApplications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedDate",
                table: "tblApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RestoredBy",
                table: "tblApplications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RestoredDate",
                table: "tblApplications",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivatedBy",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "ActivatedDate",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "CancelledBy",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "CancelledDate",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "DeactivatedBy",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "DeactivatedDate",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "IsRestored",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "RejectedBy",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "RejectedDate",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "RestoredBy",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "RestoredDate",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "ActivatedBy",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "ActivatedDate",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "CancelledBy",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "CancelledDate",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "DeactivatedBy",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "DeactivatedDate",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "IsRestored",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "RejectedBy",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "RejectedDate",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "RestoredBy",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "RestoredDate",
                table: "tblApplications");
        }
    }
}
