using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaACC
{
    public partial class UtVerificheVds
    {
        public int Id { get; set; }
        public int? IdUtente { get; set; }
        public DateTime? DataInserimento { get; set; }
        public int? IdVds { get; set; }
        public int? IdStato { get; set; }
        public DateTime? DataUltimaTaratura { get; set; }
        public DateTime? DataScadenzaTaratura { get; set; }
        public string Certificato { get; set; }
        public int? IdImpianto { get; set; }
    }
}
