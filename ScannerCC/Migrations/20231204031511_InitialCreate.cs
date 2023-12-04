using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScannerCC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoBarra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cepa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaisOrigen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaisDestino = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCosecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaProduccion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Capacidad = table.Column<int>(type: "int", nullable: true),
                    GradoAlcohol = table.Column<float>(type: "real", nullable: true),
                    Azucar = table.Column<float>(type: "real", nullable: true),
                    Sulfuroso = table.Column<float>(type: "real", nullable: true),
                    Densidad = table.Column<float>(type: "real", nullable: true),
                    TipoCapsula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoEtiqueta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorBotella = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medalla = table.Column<bool>(type: "bit", nullable: true),
                    ColorCapsula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoCorcho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoBotella = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlturaBotella = table.Column<float>(type: "real", nullable: false),
                    AnchoBotella = table.Column<float>(type: "real", nullable: false),
                    MedidadEtiquetaABoquete = table.Column<float>(type: "real", nullable: false),
                    MedidaEtiquetaABase = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.idProducto);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "idRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Escaneo",
                columns: table => new
                {
                    IdEscaneo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscaneoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escaneo", x => x.IdEscaneo);
                    table.ForeignKey(
                        name: "FK_Escaneo_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Escaneo_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioProducto",
                columns: table => new
                {
                    IdUsuarioProducto = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioProducto", x => x.IdUsuarioProducto);
                    table.ForeignKey(
                        name: "FK_UsuarioProducto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioProducto_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Escaneo_ProductoId",
                table: "Escaneo",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Escaneo_UsuarioId",
                table: "Escaneo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolId",
                table: "Usuario",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioProducto_ProductoId",
                table: "UsuarioProducto",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioProducto_UsuarioId",
                table: "UsuarioProducto",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Escaneo");

            migrationBuilder.DropTable(
                name: "UsuarioProducto");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
