using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AttrOleo.ModelsAreaACC
{
    public partial class AttrezzatureOleodinamicheAREAACCContext : DbContext
    {
        public AttrezzatureOleodinamicheAREAACCContext()
        {
        }

        public AttrezzatureOleodinamicheAREAACCContext(DbContextOptions<AttrezzatureOleodinamicheAREAACCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UtAree> UtAree { get; set; }
        public virtual DbSet<UtAttrezzature> UtAttrezzature { get; set; }
        public virtual DbSet<UtBlocchiAttrezzature> UtBlocchiAttrezzature { get; set; }
        public virtual DbSet<UtCentrali> UtCentrali { get; set; }
        public virtual DbSet<UtCostruttori> UtCostruttori { get; set; }
        public virtual DbSet<UtDischiRottura> UtDischiRottura { get; set; }
        public virtual DbSet<UtDocSil> UtDocSil { get; set; }
        public virtual DbSet<UtFlessibili> UtFlessibili { get; set; }
        public virtual DbSet<UtImpianti> UtImpianti { get; set; }
        public virtual DbSet<UtModelli> UtModelli { get; set; }
        public virtual DbSet<UtRichieste> UtRichieste { get; set; }
        public virtual DbSet<UtStatoFlessibile> UtStatoFlessibile { get; set; }
        public virtual DbSet<UtStoricoFlessibili> UtStoricoFlessibili { get; set; }
        public virtual DbSet<UtTaratureValvoleSicurezza> UtTaratureValvoleSicurezza { get; set; }
        public virtual DbSet<UtTecnici> UtTecnici { get; set; }
        public virtual DbSet<UtUtenti> UtUtenti { get; set; }
        public virtual DbSet<UtValvoleSicurezza> UtValvoleSicurezza { get; set; }
        public virtual DbSet<UtVerificheAttrezzature> UtVerificheAttrezzature { get; set; }
        public virtual DbSet<UtVerificheAttrezzatureInPressione> UtVerificheAttrezzatureInPressione { get; set; }
        public virtual DbSet<UtVerificheVds> UtVerificheVds { get; set; }
        public virtual DbSet<UtdFluido> UtdFluido { get; set; }
        public virtual DbSet<UtdStato> UtdStato { get; set; }
        public virtual DbSet<UtdTipiAttrezzature> UtdTipiAttrezzature { get; set; }
        public virtual DbSet<UtdTipiDocumento> UtdTipiDocumento { get; set; }
        public virtual DbSet<UtrAttrezzatureValvoleSicurezza> UtrAttrezzatureValvoleSicurezza { get; set; }
        public virtual DbSet<UtrAttrezzatureVerificheAttrezzature> UtrAttrezzatureVerificheAttrezzature { get; set; }
        public virtual DbSet<UtrValvoleSicurezzaTaratureValvoleSicurezza> UtrValvoleSicurezzaTaratureValvoleSicurezza { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=10.3.54.5\\SQLEXPRESS;Database=AttrezzatureOleodinamicheAREACALDO;User ID=sa;Password=12345678#A");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UtAree>(entity =>
            {
                entity.HasKey(e => e.IdArea);

                entity.ToTable("utAree");

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UtAttrezzature>(entity =>
            {
                entity.ToTable("utAttrezzature");

                entity.HasIndex(e => e.Matricola)
                    .HasName("IX_utAttrezzature");

                entity.Property(e => e.Banco).HasMaxLength(30);

                entity.Property(e => e.Censimento0).HasMaxLength(50);

                entity.Property(e => e.Cronologia0)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.DataInserimento).HasColumnType("datetime");

                entity.Property(e => e.DataLetteraSil0)
                    .HasColumnName("DataLetteraSIL0")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataPrimoImpianto).HasColumnType("datetime");

                entity.Property(e => e.DataRilascioFunzionalita).HasColumnType("datetime");

                entity.Property(e => e.DataRilascioIntegrita).HasColumnType("datetime");

                entity.Property(e => e.DataScadenzaFunzionalita).HasColumnType("datetime");

                entity.Property(e => e.DataScadenzaIntegrita).HasColumnType("datetime");

                entity.Property(e => e.Descrizione).HasMaxLength(255);

                entity.Property(e => e.IdDiscoRottura).HasColumnName("idDiscoRottura");

                entity.Property(e => e.Idv1).HasColumnName("IDV1");

                entity.Property(e => e.Idv10).HasColumnName("IDV10");

                entity.Property(e => e.Idv2).HasColumnName("IDV2");

                entity.Property(e => e.Idv3).HasColumnName("IDV3");

                entity.Property(e => e.Idv4).HasColumnName("IDV4");

                entity.Property(e => e.Idv5).HasColumnName("IDV5");

                entity.Property(e => e.Idv6).HasColumnName("IDV6");

                entity.Property(e => e.Idv7).HasColumnName("IDV7");

                entity.Property(e => e.Idv8).HasColumnName("IDV8");

                entity.Property(e => e.Idv9).HasColumnName("IDV9");

                entity.Property(e => e.Imp10)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Imp20)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.LetteraSil0)
                    .HasColumnName("LetteraSIL0")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Matricola).HasMaxLength(50);

                entity.Property(e => e.Nfabbricazione)
                    .HasColumnName("NFabbricazione")
                    .HasMaxLength(50);

                entity.Property(e => e.Norma).HasMaxLength(15);

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.PercorsoFile).HasMaxLength(255);

                entity.Property(e => e.PercorsoFileCertDisegno).HasMaxLength(255);

                entity.Property(e => e.PercorsoFileCertFunz).HasMaxLength(255);

                entity.Property(e => e.PercorsoFileCertInte).HasMaxLength(255);

                entity.Property(e => e.PercorsoFileCertSpess).HasMaxLength(255);

                entity.Property(e => e.PercorsoFileCertificato).HasMaxLength(255);

                entity.Property(e => e.PercorsoFileVarie).HasMaxLength(255);

                entity.Property(e => e.Pressione).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Pressione2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Pressione3).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Pressione4).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ubicazione0).HasMaxLength(50);

                entity.Property(e => e.UbicazioneInBlocco)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdBloccoNavigation)
                    .WithMany(p => p.UtAttrezzature)
                    .HasForeignKey(d => d.IdBlocco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_utAttrezzature_utBlocchiAttrezzature");

                entity.HasOne(d => d.IdCostruttoreNavigation)
                    .WithMany(p => p.UtAttrezzature)
                    .HasForeignKey(d => d.IdCostruttore)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_utAttrezzature_utCostruttori");

                entity.HasOne(d => d.IdDiscoRotturaNavigation)
                    .WithMany(p => p.UtAttrezzature)
                    .HasForeignKey(d => d.IdDiscoRottura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_utAttrezzature_utDischiRottura");

                entity.HasOne(d => d.IdStatoNavigation)
                    .WithMany(p => p.UtAttrezzature)
                    .HasForeignKey(d => d.IdStato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_utAttrezzature_utdStato");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.UtAttrezzature)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK_utAttrezzature_utdTipiAttrezzature");

                entity.HasOne(d => d.IdValvoleSicurezzaNavigation)
                    .WithMany(p => p.UtAttrezzature)
                    .HasForeignKey(d => d.IdValvoleSicurezza)
                    .HasConstraintName("FK_utAttrezzature_utValvoleSicurezza");
            });

            modelBuilder.Entity<UtBlocchiAttrezzature>(entity =>
            {
                entity.ToTable("utBlocchiAttrezzature");

                entity.HasIndex(e => e.Codice)
                    .HasName("IX_utBlocchiAttrezzature")
                    .IsUnique();

                entity.Property(e => e.Codice)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DataInserimento).HasColumnType("datetime");

                entity.Property(e => e.Descrizione).HasMaxLength(255);

                entity.Property(e => e.FlagIspesl)
                    .HasColumnName("FlagISPESL")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Relazione).HasMaxLength(255);

                entity.Property(e => e.SchemaCentrale).HasMaxLength(255);

                entity.HasOne(d => d.IdCentraleNavigation)
                    .WithMany(p => p.UtBlocchiAttrezzature)
                    .HasForeignKey(d => d.IdCentrale)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_utBlocchiAttrezzature_utCentrali");
            });

            modelBuilder.Entity<UtCentrali>(entity =>
            {
                entity.ToTable("utCentrali");

                entity.HasIndex(e => e.Codice)
                    .HasName("IX_utCentrali");

                entity.Property(e => e.Codice)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.DataInserimento).HasColumnType("datetime");

                entity.Property(e => e.DataPrimoImpianto).HasColumnType("datetime");

                entity.Property(e => e.Descrizione).HasMaxLength(255);

                entity.Property(e => e.Naccumulatori).HasColumnName("NAccumulatori");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.IdImpiantoNavigation)
                    .WithMany(p => p.UtCentrali)
                    .HasForeignKey(d => d.IdImpianto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_utCentrali_utImpianti");
            });

            modelBuilder.Entity<UtCostruttori>(entity =>
            {
                entity.ToTable("utCostruttori");

                entity.HasIndex(e => e.Codice)
                    .HasName("IX_utCostruttori");

                entity.Property(e => e.Codice)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Descrizione).HasMaxLength(255);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UtDischiRottura>(entity =>
            {
                entity.ToTable("utDischiRottura");

                entity.Property(e => e.Certificato).HasMaxLength(255);

                entity.Property(e => e.DataInserimento).HasColumnType("datetime");

                entity.Property(e => e.Lotto).HasMaxLength(50);

                entity.HasOne(d => d.IdCostruttoreNavigation)
                    .WithMany(p => p.UtDischiRottura)
                    .HasForeignKey(d => d.IdCostruttore)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_utDischiRottura_utCostruttori");
            });

            modelBuilder.Entity<UtDocSil>(entity =>
            {
                entity.HasKey(e => e.IdOperazione);

                entity.ToTable("utDocSIL");

                entity.Property(e => e.DataOraOperazione).HasColumnType("datetime");

                entity.Property(e => e.DataOraValidazione).HasColumnType("datetime");

                entity.Property(e => e.IdUtenteSil).HasColumnName("IdUtenteSIL");

                entity.Property(e => e.Matricola).HasMaxLength(255);

                entity.Property(e => e.NomeFile).HasMaxLength(255);

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.TipoApparecchio)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UtFlessibili>(entity =>
            {
                entity.HasKey(e => e.IdFlessibile);

                entity.ToTable("utFlessibili");

                entity.Property(e => e.CodIlva)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DataInserimento).HasColumnType("datetime");

                entity.Property(e => e.DataScadenza).HasColumnType("datetime");

                entity.Property(e => e.DataUltimaVerifica).HasColumnType("datetime");

                entity.Property(e => e.Fluido)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Lotto)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Modello)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.PressMax).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Tag)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UtImpianti>(entity =>
            {
                entity.ToTable("utImpianti");

                entity.HasIndex(e => e.Codice)
                    .HasName("IX_utImpianti")
                    .IsUnique();

                entity.Property(e => e.Codice)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Descrizione).HasMaxLength(255);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.UtImpianti)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK_utImpianti_utAree");
            });

            modelBuilder.Entity<UtModelli>(entity =>
            {
                entity.ToTable("utModelli");

                entity.Property(e => e.NomeModello).HasMaxLength(50);

                entity.HasOne(d => d.IdCostruttoreNavigation)
                    .WithMany(p => p.UtModelli)
                    .HasForeignKey(d => d.IdCostruttore)
                    .HasConstraintName("FK_utModelli_utCostruttori");
            });

            modelBuilder.Entity<UtRichieste>(entity =>
            {
                entity.ToTable("utRichieste");

                entity.Property(e => e.CognomeNome).HasMaxLength(255);

                entity.Property(e => e.DataRichiesta).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(50);

                entity.Property(e => e.TipoRichiesta).HasMaxLength(50);
            });

            modelBuilder.Entity<UtStatoFlessibile>(entity =>
            {
                entity.HasKey(e => e.IdStato);

                entity.ToTable("utStatoFlessibile");

                entity.Property(e => e.Stato)
                    .HasMaxLength(15)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UtStoricoFlessibili>(entity =>
            {
                entity.HasKey(e => e.IdStoricoFlessibili);

                entity.ToTable("utStoricoFlessibili");

                entity.Property(e => e.DataOperazione).HasColumnType("datetime");

                entity.Property(e => e.DataUltimaVerifica).HasColumnType("datetime");

                entity.Property(e => e.Lotto)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UtTaratureValvoleSicurezza>(entity =>
            {
                entity.ToTable("utTaratureValvoleSicurezza");

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.Scadenza).HasColumnType("datetime");
            });

            modelBuilder.Entity<UtTecnici>(entity =>
            {
                entity.ToTable("utTecnici");

                entity.Property(e => e.CognomeNome)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UtUtenti>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("utUtenti");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.CognomeNome).HasMaxLength(50);

                entity.Property(e => e.DataUltimoAccesso).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Utente)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UtValvoleSicurezza>(entity =>
            {
                entity.ToTable("utValvoleSicurezza");

                entity.HasIndex(e => e.Matricola)
                    .HasName("IX_utValvoleSicurezza");

                entity.Property(e => e.Certificato).HasMaxLength(255);

                entity.Property(e => e.CertificatoTaratura).HasMaxLength(255);

                entity.Property(e => e.CodIlva).HasMaxLength(7);

                entity.Property(e => e.DataInserimento).HasColumnType("datetime");

                entity.Property(e => e.DataMessaInServizio).HasColumnType("datetime");

                entity.Property(e => e.DataScadenzaTaratura).HasColumnType("datetime");

                entity.Property(e => e.Descrizione).HasMaxLength(255);

                entity.Property(e => e.Matricola)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.Pressione).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.UtValvoleSicurezza)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK_utValvoleSicurezza_utAree");

                entity.HasOne(d => d.IdImpiantoNavigation)
                    .WithMany(p => p.UtValvoleSicurezza)
                    .HasForeignKey(d => d.IdImpianto)
                    .HasConstraintName("FK_utValvoleSicurezza_utImpianti");
            });

            modelBuilder.Entity<UtVerificheAttrezzature>(entity =>
            {
                entity.ToTable("utVerificheAttrezzature");

                entity.Property(e => e.DataRilascioFunzionalita).HasColumnType("datetime");

                entity.Property(e => e.DataRilascioIntegrita).HasColumnType("datetime");

                entity.Property(e => e.DataScadenzaFunzionalita).HasColumnType("datetime");

                entity.Property(e => e.DataScadenzaIntegrita).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(255);
            });

            modelBuilder.Entity<UtVerificheAttrezzatureInPressione>(entity =>
            {
                entity.ToTable("utVerificheAttrezzatureInPressione");

                entity.Property(e => e.CertificatoFunzionalita).HasMaxLength(255);

                entity.Property(e => e.CertificatoIntegrita).HasMaxLength(255);

                entity.Property(e => e.CertificatoSpessore).HasMaxLength(255);

                entity.Property(e => e.DataInserimento).HasColumnType("datetime");

                entity.Property(e => e.DataRf)
                    .HasColumnName("DataRF")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataRi)
                    .HasColumnName("DataRI")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataSf)
                    .HasColumnName("DataSF")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataSi)
                    .HasColumnName("DataSI")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<UtVerificheVds>(entity =>
            {
                entity.ToTable("utVerificheVDS");

                entity.Property(e => e.Certificato).HasMaxLength(255);

                entity.Property(e => e.DataInserimento).HasColumnType("datetime");

                entity.Property(e => e.DataScadenzaTaratura).HasColumnType("datetime");

                entity.Property(e => e.DataUltimaTaratura).HasColumnType("datetime");

                entity.Property(e => e.IdVds).HasColumnName("IdVDS");
            });

            modelBuilder.Entity<UtdFluido>(entity =>
            {
                entity.ToTable("utdFluido");

                entity.Property(e => e.Codice)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsFixedLength();

                entity.Property(e => e.Descrizione).HasMaxLength(255);
            });

            modelBuilder.Entity<UtdStato>(entity =>
            {
                entity.ToTable("utdStato");

                entity.Property(e => e.Codice)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.Descrizione).HasMaxLength(255);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UtdTipiAttrezzature>(entity =>
            {
                entity.ToTable("utdTipiAttrezzature");

                entity.HasIndex(e => e.Codice)
                    .HasName("IX_utdTipiAttrezzature");

                entity.Property(e => e.Codice)
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.Descrizione).HasMaxLength(255);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UtdTipiDocumento>(entity =>
            {
                entity.HasKey(e => e.IdTipoDocumento);

                entity.ToTable("utdTipiDocumento");

                entity.Property(e => e.TipoDocumento).HasMaxLength(50);
            });

            modelBuilder.Entity<UtrAttrezzatureValvoleSicurezza>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("utrAttrezzature_ValvoleSicurezza");
            });

            modelBuilder.Entity<UtrAttrezzatureVerificheAttrezzature>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("utrAttrezzature_VerificheAttrezzature");
            });

            modelBuilder.Entity<UtrValvoleSicurezzaTaratureValvoleSicurezza>(entity =>
            {
                entity.HasKey(e => e.IdValvolaSicurezza);

                entity.ToTable("utrValvoleSicurezza_TaratureValvoleSicurezza");

                entity.Property(e => e.IdValvolaSicurezza).ValueGeneratedNever();

                entity.HasOne(d => d.IdTaraturaNavigation)
                    .WithMany(p => p.UtrValvoleSicurezzaTaratureValvoleSicurezza)
                    .HasForeignKey(d => d.IdTaratura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_utrValvoleSicurezza_TaratureValvoleSicurezza_utTaratureValvoleSicurezza");

                entity.HasOne(d => d.IdValvolaSicurezzaNavigation)
                    .WithOne(p => p.UtrValvoleSicurezzaTaratureValvoleSicurezza)
                    .HasForeignKey<UtrValvoleSicurezzaTaratureValvoleSicurezza>(d => d.IdValvolaSicurezza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_utrValvoleSicurezza_TaratureValvoleSicurezza_utValvoleSicurezza");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
