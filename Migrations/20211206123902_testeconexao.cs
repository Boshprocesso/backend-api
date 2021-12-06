using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webAPI.Migrations
{
    public partial class testeconexao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beneficiario",
                columns: table => new
                {
                    idBeneficiario = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nomeCompleto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    dataNascimento = table.Column<DateTime>(type: "date", nullable: true),
                    edv = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    cpf = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true, defaultValueSql: "('-')"),
                    unidade = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 10, nullable: true),
                    dataInclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    responsavelInclusao = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Benefici__09162CD11F3DC337", x => x.idBeneficiario);
                });

            migrationBuilder.CreateTable(
                name: "Beneficio",
                columns: table => new
                {
                    idBeneficio = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    descricaoBeneficio = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Benefici__00AAC26AB271CA82", x => x.idBeneficio);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    idEvento = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nomeEvento = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    descricaoEvento = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    dataInicio = table.Column<DateTime>(type: "date", nullable: false),
                    dataTermino = table.Column<DateTime>(type: "date", nullable: false),
                    inativo = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, computedColumnSql: "([dbo].[validaSeAtivo]([dataTermino]))", stored: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Evento__C8DC7BDA491E9F97", x => x.idEvento);
                });

            migrationBuilder.CreateTable(
                name: "Ilha",
                columns: table => new
                {
                    idIlha = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    descricao = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true, defaultValueSql: "('-')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ilha__B6CB724E674B9268", x => x.idIlha);
                });

            migrationBuilder.CreateTable(
                name: "Terceiro",
                columns: table => new
                {
                    idTerceiro = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    identificacao = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    dataIndicacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Terceiro__E621649430C32034", x => x.idTerceiro);
                });

            migrationBuilder.CreateTable(
                name: "EventoBeneficio",
                columns: table => new
                {
                    idEvento = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idBeneficio = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_EventoBeneficio_Beneficio",
                        column: x => x.idBeneficio,
                        principalTable: "Beneficio",
                        principalColumn: "idBeneficio");
                    table.ForeignKey(
                        name: "FK_EventoBeneficio_Evento",
                        column: x => x.idEvento,
                        principalTable: "Evento",
                        principalColumn: "idEvento");
                });

            migrationBuilder.CreateTable(
                name: "ilhaBeneficio",
                columns: table => new
                {
                    idIlha = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idBeneficio = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ilhaBeneficio_Beneficio",
                        column: x => x.idBeneficio,
                        principalTable: "Beneficio",
                        principalColumn: "idBeneficio");
                    table.ForeignKey(
                        name: "FK_ilhaBeneficio_Ilha",
                        column: x => x.idIlha,
                        principalTable: "Ilha",
                        principalColumn: "idIlha");
                });

            migrationBuilder.CreateTable(
                name: "BeneficiarioBeneficio",
                columns: table => new
                {
                    idBeneficiario = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idBeneficio = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    idTerceiro = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    entregue = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "((0))"),
                    quantidade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_BeneficiarioBeneficio_Beneficiario",
                        column: x => x.idBeneficiario,
                        principalTable: "Beneficiario",
                        principalColumn: "idBeneficiario");
                    table.ForeignKey(
                        name: "FK_BeneficiarioBeneficio_Beneficio",
                        column: x => x.idBeneficio,
                        principalTable: "Beneficio",
                        principalColumn: "idBeneficio");
                    table.ForeignKey(
                        name: "FK_BeneficiarioBeneficio_Terceiro",
                        column: x => x.idTerceiro,
                        principalTable: "Terceiro",
                        principalColumn: "idTerceiro");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiarioBeneficio_idBeneficiario",
                table: "BeneficiarioBeneficio",
                column: "idBeneficiario");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiarioBeneficio_idBeneficio",
                table: "BeneficiarioBeneficio",
                column: "idBeneficio");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiarioBeneficio_idTerceiro",
                table: "BeneficiarioBeneficio",
                column: "idTerceiro");

            migrationBuilder.CreateIndex(
                name: "IX_EventoBeneficio_idBeneficio",
                table: "EventoBeneficio",
                column: "idBeneficio");

            migrationBuilder.CreateIndex(
                name: "IX_EventoBeneficio_idEvento",
                table: "EventoBeneficio",
                column: "idEvento");

            migrationBuilder.CreateIndex(
                name: "IX_ilhaBeneficio_idBeneficio",
                table: "ilhaBeneficio",
                column: "idBeneficio");

            migrationBuilder.CreateIndex(
                name: "IX_ilhaBeneficio_idIlha",
                table: "ilhaBeneficio",
                column: "idIlha");

            migrationBuilder.CreateIndex(
                name: "UQ__Terceiro__C8F7C76B7B8EBD78",
                table: "Terceiro",
                column: "identificacao",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeneficiarioBeneficio");

            migrationBuilder.DropTable(
                name: "EventoBeneficio");

            migrationBuilder.DropTable(
                name: "ilhaBeneficio");

            migrationBuilder.DropTable(
                name: "Beneficiario");

            migrationBuilder.DropTable(
                name: "Terceiro");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Beneficio");

            migrationBuilder.DropTable(
                name: "Ilha");
        }
    }
}
