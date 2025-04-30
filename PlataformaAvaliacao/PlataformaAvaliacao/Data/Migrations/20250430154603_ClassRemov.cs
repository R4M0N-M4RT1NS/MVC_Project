using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaAvaliacao.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClassRemov : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Usuarios_AlunoId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinasOfertadas_Usuarios_AlunoId",
                table: "DisciplinasOfertadas");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinasOfertadas_AlunoId",
                table: "DisciplinasOfertadas");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_AlunoId",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "DisciplinasOfertadas");

            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "Avaliacoes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Usuarios",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AlunoId",
                table: "DisciplinasOfertadas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlunoId",
                table: "Avaliacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinasOfertadas_AlunoId",
                table: "DisciplinasOfertadas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_AlunoId",
                table: "Avaliacoes",
                column: "AlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Usuarios_AlunoId",
                table: "Avaliacoes",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinasOfertadas_Usuarios_AlunoId",
                table: "DisciplinasOfertadas",
                column: "AlunoId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
