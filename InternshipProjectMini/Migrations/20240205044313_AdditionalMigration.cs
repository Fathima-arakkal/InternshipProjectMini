using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipProjectMini.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserViewModel");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "RoleViewModel");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "UserViewModel",
                newName: "IX_UserViewModel_RoleId");

            migrationBuilder.AddColumn<bool>(
                name: "DepartmentAccess",
                table: "UserViewModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmployeeAccess",
                table: "UserViewModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LocationAccess",
                table: "UserViewModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MachineAccess",
                table: "UserViewModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserViewModel",
                table: "UserViewModel",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleViewModel",
                table: "RoleViewModel",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserViewModel_RoleViewModel_RoleId",
                table: "UserViewModel",
                column: "RoleId",
                principalTable: "RoleViewModel",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserViewModel_RoleViewModel_RoleId",
                table: "UserViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserViewModel",
                table: "UserViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleViewModel",
                table: "RoleViewModel");

            migrationBuilder.DropColumn(
                name: "DepartmentAccess",
                table: "UserViewModel");

            migrationBuilder.DropColumn(
                name: "EmployeeAccess",
                table: "UserViewModel");

            migrationBuilder.DropColumn(
                name: "LocationAccess",
                table: "UserViewModel");

            migrationBuilder.DropColumn(
                name: "MachineAccess",
                table: "UserViewModel");

            migrationBuilder.RenameTable(
                name: "UserViewModel",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "RoleViewModel",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_UserViewModel_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
