﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webAPI.DAO;

#nullable disable

namespace webAPI.Migrations
{
    [DbContext(typeof(BOSHBENEFICIOContext))]
    partial class BOSHBENEFICIOContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("webAPI.Models.Beneficiario", b =>
                {
                    b.Property<Guid>("IdBeneficiario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idBeneficiario")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Cpf")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(14)
                        .IsUnicode(false)
                        .HasColumnType("varchar(14)")
                        .HasColumnName("cpf")
                        .HasDefaultValueSql("('-')");

                    b.Property<DateTime?>("DataInclusao")
                        .HasColumnType("datetime")
                        .HasColumnName("dataInclusao");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("date")
                        .HasColumnName("dataNascimento");

                    b.Property<int?>("Edv")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("edv")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nomeCompleto");

                    b.Property<string>("ResponsavelInclusao")
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("responsavelInclusao");

                    b.Property<string>("Unidade")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("unidade");

                    b.HasKey("IdBeneficiario")
                        .HasName("PK__Benefici__09162CD11F3DC337");

                    b.ToTable("Beneficiario", (string)null);
                });

            modelBuilder.Entity("webAPI.Models.BeneficiarioBeneficio", b =>
                {
                    b.Property<string>("Entregue")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("entregue")
                        .HasDefaultValueSql("((0))")
                        .IsFixedLength();

                    b.Property<Guid?>("IdBeneficiario")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idBeneficiario");

                    b.Property<Guid?>("IdBeneficio")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idBeneficio");

                    b.Property<Guid?>("IdTerceiro")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idTerceiro");

                    b.Property<int?>("Quantidade")
                        .HasColumnType("int")
                        .HasColumnName("quantidade");

                    b.HasIndex("IdBeneficiario");

                    b.HasIndex("IdBeneficio");

                    b.HasIndex("IdTerceiro");

                    b.ToTable("BeneficiarioBeneficio", (string)null);
                });

            modelBuilder.Entity("webAPI.Models.Beneficio", b =>
                {
                    b.Property<Guid>("IdBeneficio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idBeneficio")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("DescricaoBeneficio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descricaoBeneficio");

                    b.HasKey("IdBeneficio")
                        .HasName("PK__Benefici__00AAC26AB271CA82");

                    b.ToTable("Beneficio", (string)null);
                });

            modelBuilder.Entity("webAPI.Models.Evento", b =>
                {
                    b.Property<Guid>("IdEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idEvento")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("date")
                        .HasColumnName("dataInicio");

                    b.Property<DateTime>("DataTermino")
                        .HasColumnType("date")
                        .HasColumnName("dataTermino");

                    b.Property<string>("DescricaoEvento")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descricaoEvento");

                    b.Property<string>("Inativo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("inativo")
                        .HasComputedColumnSql("([dbo].[validaSeAtivo]([dataTermino]))", false)
                        .IsFixedLength();

                    b.Property<string>("NomeEvento")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("nomeEvento");

                    b.HasKey("IdEvento")
                        .HasName("PK__Evento__C8DC7BDA491E9F97");

                    b.ToTable("Evento", (string)null);
                });

            modelBuilder.Entity("webAPI.Models.EventoBeneficio", b =>
                {
                    b.Property<Guid?>("IdBeneficio")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idBeneficio");

                    b.Property<Guid?>("IdEvento")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idEvento");

                    b.HasIndex("IdBeneficio");

                    b.HasIndex("IdEvento");

                    b.ToTable("EventoBeneficio", (string)null);
                });

            modelBuilder.Entity("webAPI.Models.Ilha", b =>
                {
                    b.Property<Guid>("IdIlha")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idIlha")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Descricao")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("descricao")
                        .HasDefaultValueSql("('-')");

                    b.HasKey("IdIlha")
                        .HasName("PK__Ilha__B6CB724E674B9268");

                    b.ToTable("Ilha", (string)null);
                });

            modelBuilder.Entity("webAPI.Models.IlhaBeneficio", b =>
                {
                    b.Property<Guid?>("IdBeneficio")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idBeneficio");

                    b.Property<Guid?>("IdIlha")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idIlha");

                    b.HasIndex("IdBeneficio");

                    b.HasIndex("IdIlha");

                    b.ToTable("ilhaBeneficio", (string)null);
                });

            modelBuilder.Entity("webAPI.Models.Terceiro", b =>
                {
                    b.Property<Guid>("IdTerceiro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("idTerceiro")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime?>("DataIndicacao")
                        .HasColumnType("datetime")
                        .HasColumnName("dataIndicacao");

                    b.Property<string>("Identificacao")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("identificacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nome");

                    b.HasKey("IdTerceiro")
                        .HasName("PK__Terceiro__E621649430C32034");

                    b.HasIndex(new[] { "Identificacao" }, "UQ__Terceiro__C8F7C76B7B8EBD78")
                        .IsUnique();

                    b.ToTable("Terceiro", (string)null);
                });

            modelBuilder.Entity("webAPI.Models.BeneficiarioBeneficio", b =>
                {
                    b.HasOne("webAPI.Models.Beneficiario", "IdBeneficiarioNavigation")
                        .WithMany()
                        .HasForeignKey("IdBeneficiario")
                        .HasConstraintName("FK_BeneficiarioBeneficio_Beneficiario");

                    b.HasOne("webAPI.Models.Beneficio", "IdBeneficioNavigation")
                        .WithMany()
                        .HasForeignKey("IdBeneficio")
                        .HasConstraintName("FK_BeneficiarioBeneficio_Beneficio");

                    b.HasOne("webAPI.Models.Terceiro", "IdTerceiroNavigation")
                        .WithMany()
                        .HasForeignKey("IdTerceiro")
                        .HasConstraintName("FK_BeneficiarioBeneficio_Terceiro");

                    b.Navigation("IdBeneficiarioNavigation");

                    b.Navigation("IdBeneficioNavigation");

                    b.Navigation("IdTerceiroNavigation");
                });

            modelBuilder.Entity("webAPI.Models.EventoBeneficio", b =>
                {
                    b.HasOne("webAPI.Models.Beneficio", "IdBeneficioNavigation")
                        .WithMany()
                        .HasForeignKey("IdBeneficio")
                        .HasConstraintName("FK_EventoBeneficio_Beneficio");

                    b.HasOne("webAPI.Models.Evento", "IdEventoNavigation")
                        .WithMany()
                        .HasForeignKey("IdEvento")
                        .HasConstraintName("FK_EventoBeneficio_Evento");

                    b.Navigation("IdBeneficioNavigation");

                    b.Navigation("IdEventoNavigation");
                });

            modelBuilder.Entity("webAPI.Models.IlhaBeneficio", b =>
                {
                    b.HasOne("webAPI.Models.Beneficio", "IdBeneficioNavigation")
                        .WithMany()
                        .HasForeignKey("IdBeneficio")
                        .HasConstraintName("FK_ilhaBeneficio_Beneficio");

                    b.HasOne("webAPI.Models.Ilha", "IdIlhaNavigation")
                        .WithMany()
                        .HasForeignKey("IdIlha")
                        .HasConstraintName("FK_ilhaBeneficio_Ilha");

                    b.Navigation("IdBeneficioNavigation");

                    b.Navigation("IdIlhaNavigation");
                });
#pragma warning restore 612, 618
        }
    }
}
