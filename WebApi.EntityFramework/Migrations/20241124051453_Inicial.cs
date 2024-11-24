using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hunter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hunter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hunter_Nen",
                columns: table => new
                {
                    Hunter_Id = table.Column<int>(type: "int", nullable: false),
                    Nen_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hunter_Nen", x => new { x.Hunter_Id, x.Nen_Id });
                    table.ForeignKey(
                        name: "FK_Hunter_Nen_Hunter_Hunter_Id",
                        column: x => x.Hunter_Id,
                        principalTable: "Hunter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hunter_Nen_Nen_Nen_Id",
                        column: x => x.Nen_Id,
                        principalTable: "Nen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Hunter",
                columns: new[] { "Id", "Age", "Name" },
                values: new object[,]
                {
                    { 1, 12, "Gon Freecss" },
                    { 2, 12, "Killua Zoldyck" },
                    { 3, 17, "Kurapika Kurta" },
                    { 4, 19, "Leorio Paladiknight" }
                });

            migrationBuilder.InsertData(
                table: "Nen",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Si un estudiante aumenta la cantidad de agua en el vaso durante su prueba del agua, es de Intensificación", "Intensificación" },
                    { 2, "Si un estudiante cambia el sabor del agua durante su prueba del agua es un Transformador", "Transformación" },
                    { 3, "Si un estudiante hace aparecer impurezas en el agua del vaso durante su prueba ellos son Materialización", "Materialización" },
                    { 4, "Si un estudiante cambia el color del agua en el vaso durante su prueba del agua, es un Emisor", "Emisión" },
                    { 5, "Si un estudiante mueve la hoja flotando en el agua del vaso durante su prueba del agua, es un Manipulador", "Manipulación" },
                    { 6, "Si un estudiante hace algún otro efecto durante su prueba del agua, son Especialistas", "Especialización" }
                });

            migrationBuilder.InsertData(
                table: "Hunter_Nen",
                columns: new[] { "Hunter_Id", "Nen_Id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 6 },
                    { 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hunter_Nen_Nen_Id",
                table: "Hunter_Nen",
                column: "Nen_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hunter_Nen");

            migrationBuilder.DropTable(
                name: "Hunter");

            migrationBuilder.DropTable(
                name: "Nen");
        }
    }
}
