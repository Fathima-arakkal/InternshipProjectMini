using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipProjectMini.Migrations
{
    /// <inheritdoc />
    public partial class Modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanAccessMachine",
                table: "Roles",
                newName: "Machine");

            migrationBuilder.RenameColumn(
                name: "CanAccessLocation",
                table: "Roles",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "CanAccessEmployee",
                table: "Roles",
                newName: "Employee");

            migrationBuilder.RenameColumn(
                name: "CanAccessDepartment",
                table: "Roles",
                newName: "Department");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Machine",
                table: "Roles",
                newName: "CanAccessMachine");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Roles",
                newName: "CanAccessLocation");

            migrationBuilder.RenameColumn(
                name: "Employee",
                table: "Roles",
                newName: "CanAccessEmployee");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Roles",
                newName: "CanAccessDepartment");
        }
    }
}
