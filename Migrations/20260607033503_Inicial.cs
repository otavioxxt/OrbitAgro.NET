using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitAgro.API.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_PRODUTOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUTOR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_AREA_CULTIVO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeArea = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Cultura = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Hectares = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    ProdutorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_AREA_CULTIVO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_AREA_CULTIVO_TB_PRODUTOR_ProdutorId",
                        column: x => x.ProdutorId,
                        principalTable: "TB_PRODUTOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ALERTA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TipoAlerta = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Observacao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    DataAlerta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    StatusAlerta = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    AreaCultivoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ALERTA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ALERTA_TB_AREA_CULTIVO_AreaCultivoId",
                        column: x => x.AreaCultivoId,
                        principalTable: "TB_AREA_CULTIVO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_MONITORAMENTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IndiceNdvi = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    NdviAnterior = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    UmidadeSolo = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    TemperaturaSolo = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DataLeitura = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    AreaCultivoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MONITORAMENTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_MONITORAMENTO_TB_AREA_CULTIVO_AreaCultivoId",
                        column: x => x.AreaCultivoId,
                        principalTable: "TB_AREA_CULTIVO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALERTA_AreaCultivoId",
                table: "TB_ALERTA",
                column: "AreaCultivoId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_AREA_CULTIVO_ProdutorId",
                table: "TB_AREA_CULTIVO",
                column: "ProdutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MONITORAMENTO_AreaCultivoId",
                table: "TB_MONITORAMENTO",
                column: "AreaCultivoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ALERTA");

            migrationBuilder.DropTable(
                name: "TB_MONITORAMENTO");

            migrationBuilder.DropTable(
                name: "TB_AREA_CULTIVO");

            migrationBuilder.DropTable(
                name: "TB_PRODUTOR");
        }
    }
}
