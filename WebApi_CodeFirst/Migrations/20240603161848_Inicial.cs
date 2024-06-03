using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi_CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cazadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cazadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Descripcion = table.Column<string>(type: "VARCHAR(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CazadorNen",
                columns: table => new
                {
                    Id_Cazador = table.Column<int>(type: "int", nullable: false),
                    Id_Nen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CazadorNen", x => new { x.Id_Cazador, x.Id_Nen });
                    table.ForeignKey(
                        name: "FK_CazadorNen_Cazadores_Id_Cazador",
                        column: x => x.Id_Cazador,
                        principalTable: "Cazadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CazadorNen_Nen_Id_Nen",
                        column: x => x.Id_Nen,
                        principalTable: "Nen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cazadores",
                columns: new[] { "Id", "Edad", "Nombre" },
                values: new object[,]
                {
                    { 1, 12, "Gon Freecss" },
                    { 2, 12, "Killua Zoldyck" },
                    { 3, 17, "Kurapika Kurta" },
                    { 4, 19, "Leorio Paladiknight" }
                });

            migrationBuilder.InsertData(
                table: "Nen",
                columns: new[] { "Id", "Descripcion", "Nombre" },
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
                table: "CazadorNen",
                columns: new[] { "Id_Cazador", "Id_Nen" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 6 },
                    { 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CazadorNen_Id_Nen",
                table: "CazadorNen",
                column: "Id_Nen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CazadorNen");

            migrationBuilder.DropTable(
                name: "Cazadores");

            migrationBuilder.DropTable(
                name: "Nen");
        }
    }
}
