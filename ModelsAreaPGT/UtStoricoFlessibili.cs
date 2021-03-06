using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaPGT
{
    public partial class UtStoricoFlessibili
    {
        public int IdStoricoFlessibili { get; set; }
        public string Lotto { get; set; }
        public DateTime? DataUltimaVerifica { get; set; }
        public int? IdUtente { get; set; }
        public DateTime? DataOperazione { get; set; }
    }
}
