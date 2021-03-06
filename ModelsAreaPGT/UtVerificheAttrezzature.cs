using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaPGT
{
    public partial class UtVerificheAttrezzature
    {
        public int Id { get; set; }
        public DateTime? DataRilascioFunzionalita { get; set; }
        public DateTime? DataScadenzaFunzionalita { get; set; }
        public DateTime? DataRilascioIntegrita { get; set; }
        public DateTime? DataScadenzaIntegrita { get; set; }
        public bool PrimoImpianto { get; set; }
        public string Note { get; set; }
    }
}
