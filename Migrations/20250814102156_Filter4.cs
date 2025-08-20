using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employewebapp.Migrations
{
    /// <inheritdoc />
    public partial class Filter4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Severity",
                table: "ExceptionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ExceptionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Severity",
                table: "ExceptionLogs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ExceptionLogs");
        }
    }
}
