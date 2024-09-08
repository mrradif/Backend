using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Context.Control_Panel.Migrations
{
    /// <inheritdoc />
    public partial class ControlPanelInit1August2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblUserCreationConfigs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblUserCreationConfigs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblSubMenus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblSubMenus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblPageTabs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblPageTabs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblOrganizations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblOrganizations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblOrganizationPolicyConfigs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblOrganizationPolicyConfigs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblModules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblModules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblMainMenus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblMainMenus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblDivisions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblDivisions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblCompanyPolicyConfigs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblCompanyPolicyConfigs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblCompanies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblCompanies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblBranches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblBranches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "tblApplications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "tblApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblUserCreationConfigs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblUserCreationConfigs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblSubMenus");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblSubMenus");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblPageTabs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblPageTabs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblOrganizations");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblOrganizations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblOrganizationPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblModules");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblMainMenus");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblMainMenus");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblDivisions");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblDivisions");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblCompanyPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblCompanyPolicyConfigs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblCompanies");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblCompanies");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblBranches");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblBranches");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "tblApplications");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AspNetRoles");
        }
    }
}
