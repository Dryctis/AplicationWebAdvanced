using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppWebAvanzada.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposNuevosIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ciudad",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "direccion",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pais",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ciudad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "direccion",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "pais",
                table: "AspNetUsers");
        }
    }
}
