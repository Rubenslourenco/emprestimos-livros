using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emprestimos_livros.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarRecebedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Recebedor",
                table: "Emprestimos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recebedor",
                table: "Emprestimos");
        }
    }
}
