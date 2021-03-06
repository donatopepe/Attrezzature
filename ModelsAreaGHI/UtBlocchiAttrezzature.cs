using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaGHI
{
    public partial class UtBlocchiAttrezzature
    {
        public UtBlocchiAttrezzature()
        {
            UtAttrezzature = new HashSet<UtAttrezzature>();
        }

        public int Id { get; set; }
        public string Codice { get; set; }
        public string Nome { get; set; }
        public int IdCentrale { get; set; }
        public string Descrizione { get; set; }
        public string SchemaCentrale { get; set; }
        public string Relazione { get; set; }
        public int? IdUtente { get; set; }
        public DateTime? DataInserimento { get; set; }
        public string FlagIspesl { get; set; }

        public virtual UtCentrali IdCentraleNavigation { get; set; }
        public virtual ICollection<UtAttrezzature> UtAttrezzature { get; set; }
    }
}
