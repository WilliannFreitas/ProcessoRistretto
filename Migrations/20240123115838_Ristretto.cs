using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessoRistretto.Migrations
{
    public partial class Ristretto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    ID_EMPRESA = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME_EMPRESARIAL = table.Column<string>(nullable: true),
                    TELEFONE_1 = table.Column<long>(nullable: false),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.ID_EMPRESA);
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIO",
                columns: table => new
                {
                    ID_FUNCIONARIO = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(nullable: true),
                    SOBRENOME = table.Column<string>(nullable: true),
                    LOGIN = table.Column<string>(nullable: true),
                    SENHA = table.Column<long>(nullable: false),
                    DT_NASCIMENTO = table.Column<DateTime>(nullable: false),
                    STATUS = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIO", x => x.ID_FUNCIONARIO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPRESA");

            migrationBuilder.DropTable(
                name: "FUNCIONARIO");
        }
    }
}
