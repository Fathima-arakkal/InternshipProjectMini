using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipProjectMini.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPermissionViewModel",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionViewModel", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignedModules = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPermissionViewModelUserName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.RoleName);
                    table.ForeignKey(
                        name: "FK_RolePermission_UserPermissionViewModel_UserPermissionViewModelUserName",
                        column: x => x.UserPermissionViewModelUserName,
                        principalTable: "UserPermissionViewModel",
                        principalColumn: "UserName");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_UserPermissionViewModelUserName",
                table: "RolePermission",
                column: "UserPermissionViewModelUserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "UserPermissionViewModel");
        }
    }
}
