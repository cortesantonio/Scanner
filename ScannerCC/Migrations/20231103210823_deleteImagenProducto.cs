using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScannerCC.Migrations
{
    /// <inheritdoc />
    public partial class deleteImagenProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Producto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "Producto",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
