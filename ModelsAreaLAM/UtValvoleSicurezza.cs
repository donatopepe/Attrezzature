using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaLAM
{
    public partial class UtValvoleSicurezza
    {
        public UtValvoleSicurezza()
        {
            UtAttrezzature = new HashSet<UtAttrezzature>();
        }

        public int Id { get; set; }
        public string Matricola { get; set; }
        public int IdCostruttore { get; set; }
        public decimal? Pressione { get; set; }
        public int? IdStato { get; set; }
        public string Descrizione { get; set; }
        public int? IdModello { get; set; }
        public int? IdUtente { get; set; }
        public DateTime? DataInserimento { get; set; }
        public int? IdArea { get; set; }
        public int? IdImpianto { get; set; }
        public DateTime? DataMessaInServizio { get; set; }
        public DateTime? DataScadenzaTaratura { get; set; }
        public string Note { get; set; }
        public bool? Documentazione { get; set; }
        public string Certificato { get; set; }
        public string CertificatoTaratura { get; set; }
        public string CodIlva { get; set; }

        public virtual UtrValvoleSicurezzaTaratureValvoleSicurezza UtrValvoleSicurezzaTaratureValvoleSicurezza { get; set; }
        public virtual ICollection<UtAttrezzature> UtAttrezzature { get; set; }
    }
}
