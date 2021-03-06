using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaGHI
{
    public partial class UtFlessibili
    {
        public int IdFlessibile { get; set; }
        public string Lotto { get; set; }
        public int? IdCostruttore { get; set; }
        public decimal? PressMax { get; set; }
        public DateTime? DataUltimaVerifica { get; set; }
        public DateTime? DataScadenza { get; set; }
        public int? IdStato { get; set; }
        public string Tag { get; set; }
        public string Modello { get; set; }
        public string Note { get; set; }
        public string Fluido { get; set; }
        public string CodIlva { get; set; }
        public int? IdUtente { get; set; }
        public DateTime? DataInserimento { get; set; }
    }
}
