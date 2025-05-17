using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserForeignKeyForRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_CompanyId",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_DriverId",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_CompanyId",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_DriverId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "RefreshToken");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "RefreshToken",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_User_UserId",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RefreshToken");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "RefreshToken",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "RefreshToken",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_CompanyId",
                table: "RefreshToken",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_DriverId",
                table: "RefreshToken",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_CompanyId",
                table: "RefreshToken",
                column: "CompanyId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_User_DriverId",
                table: "RefreshToken",
                column: "DriverId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
