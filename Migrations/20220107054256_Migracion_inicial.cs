using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pizza_App.Migrations
{
    public partial class Migracion_inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pizzas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_pizza = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cantidad_porciones = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    precio_unitario = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    tamano = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pizzas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    tipo_usuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ordenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_solicitante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    pizzaId = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    fecha_orden = table.Column<DateTime>(type: "datetime2", nullable: false),
                    direccion_envio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    comentarios = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    estado_orden = table.Column<int>(type: "int", nullable: false),
                    usuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ordenes_pizzas_pizzaId",
                        column: x => x.pizzaId,
                        principalTable: "pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordenes_usuarios_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_pizzaId",
                table: "ordenes",
                column: "pizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_usuarioId",
                table: "ordenes",
                column: "usuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ordenes");

            migrationBuilder.DropTable(
                name: "pizzas");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
