using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using webAPI.Models;

namespace webAPI.DAO
{
    public partial class BOSHBENEFICIOContext : DbContext
    {
        public BOSHBENEFICIOContext()
        {
        }

        public BOSHBENEFICIOContext(DbContextOptions<BOSHBENEFICIOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Beneficiario> Beneficiarios { get; set; } = null!;
        public virtual DbSet<BeneficiarioBeneficio> BeneficiarioBeneficios { get; set; } = null!;
        public virtual DbSet<Beneficio> Beneficios { get; set; } = null!;
        public virtual DbSet<Evento> Eventos { get; set; } = null!;
        public virtual DbSet<EventoBeneficio> EventoBeneficios { get; set; } = null!;
        public virtual DbSet<Ilha> Ilhas { get; set; } = null!;
        public virtual DbSet<IlhaBeneficio> IlhaBeneficios { get; set; } = null!;
        public virtual DbSet<Terceiro> Terceiros { get; set; } = null!;
        public virtual DbSet<EventoBeneficiario> EventoBeneficiarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Data Source=LAPTOP-7IG3QRH4;Initial Catalog=BOSHBENEFICIO");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beneficiario>(entity =>
            {
                entity.HasKey(e => e.IdBeneficiario)
                    .HasName("PK__Benefici__09162CD1817DC479");

                entity.ToTable("Beneficiario");

                entity.Property(e => e.IdBeneficiario)
                    .HasColumnName("idBeneficiario")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("cpf")
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.DataInclusao)
                    .HasColumnType("datetime")
                    .HasColumnName("dataInclusao");

                entity.Property(e => e.DataNascimento)
                    .HasColumnType("date")
                    .HasColumnName("dataNascimento");

                entity.Property(e => e.Edv)
                    .HasColumnName("edv")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NomeCompleto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nomeCompleto");

                entity.Property(e => e.ResponsavelInclusao)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("responsavelInclusao");

                entity.Property(e => e.Unidade)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("unidade");
            });

            modelBuilder.Entity<BeneficiarioBeneficio>(entity =>
            {
                entity.HasKey(e => new { e.IdBeneficiario, e.IdBeneficio });

                entity.ToTable("BeneficiarioBeneficio");

                entity.Property(e => e.Entregue)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("entregue")
                    .HasDefaultValueSql("((0))")
                    .IsFixedLength();

                entity.Property(e => e.IdBeneficiario).HasColumnName("idBeneficiario");

                entity.Property(e => e.IdBeneficio).HasColumnName("idBeneficio");

                entity.Property(e => e.IdTerceiro).HasColumnName("idTerceiro");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.HasOne(d => d.IdBeneficiarioNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdBeneficiario)
                    .HasConstraintName("FK_BeneficiarioBeneficio_Beneficiario");

                entity.HasOne(d => d.IdBeneficioNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdBeneficio)
                    .HasConstraintName("FK_BeneficiarioBeneficio_Beneficio");

                entity.HasOne(d => d.IdTerceiroNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdTerceiro)
                    .HasConstraintName("FK_BeneficiarioBeneficio_Terceiro");
            });

            modelBuilder.Entity<Beneficio>(entity =>
            {
                entity.HasKey(e => e.IdBeneficio)
                    .HasName("PK__Benefici__00AAC26A4746E772");

                entity.ToTable("Beneficio");

                entity.Property(e => e.IdBeneficio)
                    .HasColumnName("idBeneficio")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DescricaoBeneficio)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descricaoBeneficio");
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.IdEvento)
                    .HasName("PK__Evento__C8DC7BDAA64D1C18");

                entity.ToTable("Evento");

                entity.Property(e => e.IdEvento)
                    .HasColumnName("idEvento")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DataInicio)
                    .HasColumnType("date")
                    .HasColumnName("dataInicio");

                entity.Property(e => e.DataTermino)
                    .HasColumnType("date")
                    .HasColumnName("dataTermino");

                entity.Property(e => e.DescricaoEvento)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descricaoEvento");

                entity.Property(e => e.Inativo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("inativo")
                    .HasComputedColumnSql("([dbo].[validaSeAtivo]([dataTermino]))", false)
                    .IsFixedLength();

                entity.Property(e => e.NomeEvento)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nomeEvento");
            });

            modelBuilder.Entity<EventoBeneficiario>(entity =>
            {
                entity.HasKey(e => new {e.IdEvento, e.IdBeneficiario});

                entity.ToTable("EventoBeneficiario");

                entity.Property(e => e.IdBeneficiario).HasColumnName("idBeneficiario");

                entity.Property(e => e.IdEvento).HasColumnName("idEvento");

                entity.HasOne(d => d.IdBeneficiarioNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdBeneficiario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventoBeneficiario_Beneficiario");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventoBeneficiario_Evento");
            });

            modelBuilder.Entity<EventoBeneficio>(entity =>
            {
                entity.HasKey(e => new {e.IdEvento, e.IdBeneficio});

                entity.ToTable("EventoBeneficio");

                entity.Property(e => e.IdBeneficio).HasColumnName("idBeneficio");

                entity.Property(e => e.IdEvento).HasColumnName("idEvento");

                entity.HasOne(d => d.IdBeneficioNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdBeneficio)
                    .HasConstraintName("FK_EventoBeneficio_Beneficio");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdEvento)
                    .HasConstraintName("FK_EventoBeneficio_Evento");
            });

            modelBuilder.Entity<Ilha>(entity =>
            {
                entity.HasKey(e => e.IdIlha)
                    .HasName("PK__Ilha__B6CB724ED4375913");

                entity.ToTable("Ilha");

                entity.Property(e => e.IdIlha)
                    .HasColumnName("idIlha")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("descricao")
                    .HasDefaultValueSql("('-')");
            });

            modelBuilder.Entity<IlhaBeneficio>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ilhaBeneficio");

                entity.Property(e => e.IdBeneficio).HasColumnName("idBeneficio");

                entity.Property(e => e.IdIlha).HasColumnName("idIlha");

                entity.HasOne(d => d.IdBeneficioNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdBeneficio)
                    .HasConstraintName("FK_ilhaBeneficio_Beneficio");

                entity.HasOne(d => d.IdIlhaNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdIlha)
                    .HasConstraintName("FK_ilhaBeneficio_Ilha");
            });

            modelBuilder.Entity<Terceiro>(entity =>
            {
                entity.HasKey(e => e.IdTerceiro)
                    .HasName("PK__Terceiro__E621649410F9B6F1");

                entity.ToTable("Terceiro");

                entity.HasIndex(e => e.Identificacao, "UQ__Terceiro__C8F7C76B693A4546")
                    .IsUnique();

                entity.Property(e => e.IdTerceiro)
                    .HasColumnName("idTerceiro")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DataIndicacao)
                    .HasColumnType("datetime")
                    .HasColumnName("dataIndicacao");

                entity.Property(e => e.Identificacao)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("identificacao");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
        // METODOS
        
    }

    
}
