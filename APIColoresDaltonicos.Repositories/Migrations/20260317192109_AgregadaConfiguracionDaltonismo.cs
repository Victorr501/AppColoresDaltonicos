using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIColoresDaltonicos.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AgregadaConfiguracionDaltonismo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Correccion",
                table: "ConfiguracionesDaltonismo",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Correccion",
                table: "ConfiguracionesDaltonismo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
