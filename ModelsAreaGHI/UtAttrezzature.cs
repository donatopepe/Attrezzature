using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaGHI
{
    public partial class UtAttrezzature
    {
        public int Id { get; set; }
        public int? IdTipo { get; set; }
        public string Matricola { get; set; }
        public string Nfabbricazione { get; set; }
        public decimal? Pressione { get; set; }
        public int? Volume { get; set; }
        public int? IdFluido { get; set; }
        public string Norma { get; set; }
        public bool? Documentazione { get; set; }
        public DateTime? DataRilascioFunzionalita { get; set; }
        public DateTime? DataScadenzaFunzionalita { get; set; }
        public DateTime? DataRilascioIntegrita { get; set; }
        public DateTime? DataScadenzaIntegrita { get; set; }
        public int? IdUtente { get; set; }
        public int IdStato { get; set; }
        public int IdBlocco { get; set; }
        public int IdCostruttore { get; set; }
        public int IdDiscoRottura { get; set; }
        public int? IdValvoleSicurezza { get; set; }
        public int? IdValvoleSicurezzaScorta { get; set; }
        public int? IdTecnico { get; set; }
        public string UbicazioneInBlocco { get; set; }
        public string Descrizione { get; set; }
        public string Note { get; set; }
        public string Ubicazione0 { get; set; }
        public int? Pos0 { get; set; }
        public string LetteraSil0 { get; set; }
        public DateTime? DataLetteraSil0 { get; set; }
        public string Cronologia0 { get; set; }
        public string Imp10 { get; set; }
        public string Imp20 { get; set; }
        public string Censimento0 { get; set; }
        public string Banco { get; set; }
        public int? Posizione { get; set; }
        public DateTime? DataInserimento { get; set; }
        public string PercorsoFile { get; set; }
        public string PercorsoFileCertificato { get; set; }
        public string PercorsoFileCertFunz { get; set; }
        public string PercorsoFileCertInte { get; set; }
        public string PercorsoFileCertSpess { get; set; }
        public string PercorsoFileCertDisegno { get; set; }
        public DateTime? DataPrimoImpianto { get; set; }
        public int? Idv1 { get; set; }
        public int? Idv2 { get; set; }
        public int? Idv3 { get; set; }
        public int? Idv4 { get; set; }
        public int? Idv5 { get; set; }
        public int? Idv6 { get; set; }
        public int? Idv7 { get; set; }
        public int? Idv8 { get; set; }
        public int? Idv9 { get; set; }
        public int? Idv10 { get; set; }
        public decimal? Pressione2 { get; set; }
        public decimal? Pressione3 { get; set; }
        public decimal? Pressione4 { get; set; }
        public string PercorsoFileVarie { get; set; }
        public int? AnnoFabbricazione { get; set; }
        public int? PrimaPeriodica { get; set; }

        public virtual UtBlocchiAttrezzature IdBloccoNavigation { get; set; }
        public virtual UtCostruttori IdCostruttoreNavigation { get; set; }
        public virtual UtDischiRottura IdDiscoRotturaNavigation { get; set; }
        public virtual UtdStato IdStatoNavigation { get; set; }
        public virtual UtdTipiAttrezzature IdTipoNavigation { get; set; }
        public virtual UtValvoleSicurezza IdValvoleSicurezzaNavigation { get; set; }
    }
}
