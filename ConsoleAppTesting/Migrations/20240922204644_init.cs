using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleAppTesting.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Secrets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IV1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IV2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IV3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_User = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secrets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SqlToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Secrets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
