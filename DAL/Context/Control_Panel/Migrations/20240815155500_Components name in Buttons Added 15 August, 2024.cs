using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Context.Control_Panel.Migrations
{
    /// <inheritdoc />
    public partial class ComponentsnameinButtonsAdded15August2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComponentName",
                table: "tblButtons",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponentName",
                table: "tblButtons");
        }
    }
}
