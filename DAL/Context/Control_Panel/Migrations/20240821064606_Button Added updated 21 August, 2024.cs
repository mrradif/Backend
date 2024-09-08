using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Context.Control_Panel.Migrations
{
    /// <inheritdoc />
    public partial class ButtonAddedupdated21August2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserButtons_AspNetUsers_UserId",
                table: "UserButtons");

            migrationBuilder.DropForeignKey(
                name: "FK_UserButtons_tblButtons_ButtonId",
                table: "UserButtons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserButtons",
                table: "UserButtons");

            migrationBuilder.RenameTable(
                name: "UserButtons",
                newName: "tblUserButtons");

            migrationBuilder.RenameIndex(
                name: "IX_UserButtons_UserId",
                table: "tblUserButtons",
                newName: "IX_tblUserButtons_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserButtons_ButtonId",
                table: "tblUserButtons",
                newName: "IX_tblUserButtons_ButtonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblUserButtons",
                table: "tblUserButtons",
                column: "UserButtonId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserButtons_AspNetUsers_UserId",
                table: "tblUserButtons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserButtons_tblButtons_ButtonId",
                table: "tblUserButtons",
                column: "ButtonId",
                principalTable: "tblButtons",
                principalColumn: "ButtonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUserButtons_AspNetUsers_UserId",
                table: "tblUserButtons");

            migrationBuilder.DropForeignKey(
                name: "FK_tblUserButtons_tblButtons_ButtonId",
                table: "tblUserButtons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblUserButtons",
                table: "tblUserButtons");

            migrationBuilder.RenameTable(
                name: "tblUserButtons",
                newName: "UserButtons");

            migrationBuilder.RenameIndex(
                name: "IX_tblUserButtons_UserId",
                table: "UserButtons",
                newName: "IX_UserButtons_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tblUserButtons_ButtonId",
                table: "UserButtons",
                newName: "IX_UserButtons_ButtonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserButtons",
                table: "UserButtons",
                column: "UserButtonId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserButtons_AspNetUsers_UserId",
                table: "UserButtons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserButtons_tblButtons_ButtonId",
                table: "UserButtons",
                column: "ButtonId",
                principalTable: "tblButtons",
                principalColumn: "ButtonId");
        }
    }
}
