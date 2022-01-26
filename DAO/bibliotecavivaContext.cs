using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class bibliotecavivaContext : DbContext
    {
        public bibliotecavivaContext()
        {
        }

        public bibliotecavivaContext(DbContextOptions<bibliotecavivaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apelido> Apelidos { get; set; }
        public virtual DbSet<Descricao> Descricaos { get; set; }
        public virtual DbSet<Idioma> Idiomas { get; set; }
        public virtual DbSet<Localizacaogeografica> Localizacaogeograficas { get; set; }
        public virtual DbSet<Pessoa> Pessoas { get; set; }
        public virtual DbSet<Pessoaapelido> Pessoaapelidos { get; set; }
        public virtual DbSet<Pessoaregistro> Pessoaregistros { get; set; }
        public virtual DbSet<Referencium> Referencia { get; set; }
        public virtual DbSet<Registro> Registros { get; set; }
        public virtual DbSet<Registroapelido> Registroapelidos { get; set; }
        public virtual DbSet<Registrolocalizacao> Registrolocalizacaos { get; set; }
        public virtual DbSet<Tipo> Tipos { get; set; }
        public virtual DbSet<Tipodeexecucao> Tipodeexecucaos { get; set; }
        public virtual DbSet<Tiporelacao> Tiporelacaos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("User ID=postgres;Password=senha;Server=127.0.0.1;Port=5432;Database=bibliotecaviva;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Portuguese_Brazil.1252");

            modelBuilder.Entity<Apelido>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("apelido_pkey");

                entity.ToTable("apelido");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Descricao>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("descricao_pkey");

                entity.ToTable("descricao");

                entity.HasIndex(e => e.Registro, "descricao_fkindex1");

                entity.HasIndex(e => e.Registro, "ifk_rel_14");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Conteudo)
                    .IsRequired()
                    .HasColumnName("conteudo");

                entity.Property(e => e.Registro).HasColumnName("registro");

                entity.HasOne(d => d.RegistroNavigation)
                    .WithMany(p => p.Descricaos)
                    .HasForeignKey(d => d.Registro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("descricao_registro_fkey");
            });

            modelBuilder.Entity<Idioma>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("idioma_pkey");

                entity.ToTable("idioma");

                entity.HasIndex(e => e.Nome, "idioma_nome_key")
                    .IsUnique();

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Localizacaogeografica>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("localizacaogeografica_pkey");

                entity.ToTable("localizacaogeografica");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pessoa_pkey");

                entity.ToTable("pessoa");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("genero");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("nome");

                entity.Property(e => e.Sobrenome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("sobrenome");
            });

            modelBuilder.Entity<Pessoaapelido>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pessoaapelido_pkey");

                entity.ToTable("pessoaapelido");

                entity.HasIndex(e => e.Pessoa, "ifk_rel_07");

                entity.HasIndex(e => e.Apelido, "ifk_rel_08");

                entity.HasIndex(e => e.Pessoa, "pessoaapelido_fkindex1");

                entity.HasIndex(e => e.Apelido, "pessoaapelido_fkindex2");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Apelido).HasColumnName("apelido");

                entity.Property(e => e.Pessoa).HasColumnName("pessoa");

                entity.HasOne(d => d.ApelidoNavigation)
                    .WithMany(p => p.Pessoaapelidos)
                    .HasForeignKey(d => d.Apelido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pessoaapelido_apelido_fkey");

                entity.HasOne(d => d.PessoaNavigation)
                    .WithMany(p => p.Pessoaapelidos)
                    .HasForeignKey(d => d.Pessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pessoaapelido_pessoa_fkey");
            });

            modelBuilder.Entity<Pessoaregistro>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pessoaregistro_pkey");

                entity.ToTable("pessoaregistro");

                entity.HasIndex(e => e.Pessoa, "ifk_rel_09");

                entity.HasIndex(e => e.Registro, "ifk_rel_10");

                entity.HasIndex(e => e.Tiporelacao, "ifk_rel_11");

                entity.HasIndex(e => e.Pessoa, "pessoadocumento_fkindex1");

                entity.HasIndex(e => e.Registro, "pessoadocumento_fkindex2");

                entity.HasIndex(e => e.Tiporelacao, "pessoadocumento_fkindex3");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Pessoa).HasColumnName("pessoa");

                entity.Property(e => e.Registro).HasColumnName("registro");

                entity.Property(e => e.Tiporelacao).HasColumnName("tiporelacao");

                entity.HasOne(d => d.PessoaNavigation)
                    .WithMany(p => p.Pessoaregistros)
                    .HasForeignKey(d => d.Pessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pessoaregistro_pessoa_fkey");

                entity.HasOne(d => d.RegistroNavigation)
                    .WithMany(p => p.Pessoaregistros)
                    .HasForeignKey(d => d.Registro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pessoaregistro_registro_fkey");

                entity.HasOne(d => d.TiporelacaoNavigation)
                    .WithMany(p => p.Pessoaregistros)
                    .HasForeignKey(d => d.Tiporelacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pessoaregistro_tiporelacao_fkey");
            });

            modelBuilder.Entity<Referencium>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("referencia_pkey");

                entity.ToTable("referencia");

                entity.HasIndex(e => e.Registro, "ifk_rel_24");

                entity.HasIndex(e => e.Referencia, "ifk_rel_25");

                entity.HasIndex(e => e.Registro, "registrorelacionado_fkindex1");

                entity.HasIndex(e => e.Referencia, "registrorelacionado_fkindex2");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Referencia).HasColumnName("referencia");

                entity.Property(e => e.Registro).HasColumnName("registro");

                entity.HasOne(d => d.ReferenciaNavigation)
                    .WithMany(p => p.ReferenciumReferenciaNavigations)
                    .HasForeignKey(d => d.Referencia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("referencia_referencia_fkey");

                entity.HasOne(d => d.RegistroNavigation)
                    .WithMany(p => p.ReferenciumRegistroNavigations)
                    .HasForeignKey(d => d.Registro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("referencia_registro_fkey");
            });

            modelBuilder.Entity<Registro>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("registro_pkey");

                entity.ToTable("registro");

                entity.HasIndex(e => e.Tipo, "ifk_rel_17");

                entity.HasIndex(e => e.Idioma, "ifk_rel_23");

                entity.HasIndex(e => e.Tipo, "registro_fkindex1");

                entity.HasIndex(e => e.Idioma, "registro_fkindex2");

                entity.HasIndex(e => e.Nome, "registro_nome_key")
                    .IsUnique();

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Conteudo)
                    .IsRequired()
                    .HasColumnName("conteudo");

                entity.Property(e => e.Datainsercao).HasColumnName("datainsercao");

                entity.Property(e => e.Idioma).HasColumnName("idioma");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("nome");

                entity.Property(e => e.Tipo).HasColumnName("tipo");

                entity.HasOne(d => d.IdiomaNavigation)
                    .WithMany(p => p.Registros)
                    .HasForeignKey(d => d.Idioma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("registro_idioma_fkey");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.Registros)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("registro_tipo_fkey");
            });

            modelBuilder.Entity<Registroapelido>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("registroapelido_pkey");

                entity.ToTable("registroapelido");

                entity.HasIndex(e => e.Apelido, "ifk_rel_12");

                entity.HasIndex(e => e.Registro, "ifk_rel_13");

                entity.HasIndex(e => e.Apelido, "registroapelido_fkindex1");

                entity.HasIndex(e => e.Registro, "registroapelido_fkindex2");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Apelido).HasColumnName("apelido");

                entity.Property(e => e.Registro).HasColumnName("registro");

                entity.HasOne(d => d.ApelidoNavigation)
                    .WithMany(p => p.Registroapelidos)
                    .HasForeignKey(d => d.Apelido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("registroapelido_apelido_fkey");

                entity.HasOne(d => d.RegistroNavigation)
                    .WithMany(p => p.Registroapelidos)
                    .HasForeignKey(d => d.Registro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("registroapelido_registro_fkey");
            });

            modelBuilder.Entity<Registrolocalizacao>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("registrolocalizacao_pkey");

                entity.ToTable("registrolocalizacao");

                entity.HasIndex(e => e.Localizacaogeografica, "ifk_rel_21");

                entity.HasIndex(e => e.Registro, "ifk_rel_22");

                entity.HasIndex(e => e.Localizacaogeografica, "registrolocalizacao_fkindex1");

                entity.HasIndex(e => e.Registro, "registrolocalizacao_fkindex2");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Localizacaogeografica).HasColumnName("localizacaogeografica");

                entity.Property(e => e.Registro).HasColumnName("registro");

                entity.HasOne(d => d.LocalizacaogeograficaNavigation)
                    .WithMany(p => p.Registrolocalizacaos)
                    .HasForeignKey(d => d.Localizacaogeografica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("registrolocalizacao_localizacaogeografica_fkey");

                entity.HasOne(d => d.RegistroNavigation)
                    .WithMany(p => p.Registrolocalizacaos)
                    .HasForeignKey(d => d.Registro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("registrolocalizacao_registro_fkey");
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("tipo_pkey");

                entity.ToTable("tipo");

                entity.HasIndex(e => e.Tipodeexecucao, "ifk_rel_27");

                entity.HasIndex(e => e.Tipodeexecucao, "tipo_fkindex1");

                entity.HasIndex(e => e.Nome, "tipo_nome_key")
                    .IsUnique();

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Extensao)
                    .IsRequired()
                    .HasMaxLength(7)
                    .HasColumnName("extensao");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("nome");

                entity.Property(e => e.Tipodeexecucao).HasColumnName("tipodeexecucao");

                entity.HasOne(d => d.TipodeexecucaoNavigation)
                    .WithMany(p => p.Tipos)
                    .HasForeignKey(d => d.Tipodeexecucao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tipo_tipodeexecucao_fkey");
            });

            modelBuilder.Entity<Tipodeexecucao>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("tipodeexecucao_pkey");

                entity.ToTable("tipodeexecucao");

                entity.HasIndex(e => e.Nome, "tipodeexecucao_nome_key")
                    .IsUnique();

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Binario).HasColumnName("binario");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("nome");
            });

            modelBuilder.Entity<Tiporelacao>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("tiporelacao_pkey");

                entity.ToTable("tiporelacao");

                entity.HasIndex(e => e.Nome, "tiporelacao_nome_key")
                    .IsUnique();

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nome");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
