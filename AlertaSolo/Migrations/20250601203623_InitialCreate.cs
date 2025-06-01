using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlertaSolo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    Idade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Uf = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalRisco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeLocal = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Latitude = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Longitude = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Uf = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    GrauRisco = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Ativo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalRisco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalRisco_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CodigoEsp32 = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    TipoSensor = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DataInstalacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    QntdAlertas = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LocalRiscoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensor_LocalRisco_LocalRiscoId",
                        column: x => x.LocalRiscoId,
                        principalTable: "LocalRisco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalRisco_UsuarioId",
                table: "LocalRisco",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_LocalRiscoId",
                table: "Sensor",
                column: "LocalRiscoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sensor");

            migrationBuilder.DropTable(
                name: "LocalRisco");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
