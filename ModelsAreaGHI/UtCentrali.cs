using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaGHI
{
    public partial class UtCentrali
    {
        public UtCentrali()
        {
            UtBlocchiAttrezzature = new HashSet<UtBlocchiAttrezzature>();
        }

        public int Id { get; set; }
        public string Codice { get; set; }
        public string Nome { get; set; }
        public int IdImpianto { get; set; }
        public string Descrizione { get; set; }
        public bool? PrimoImpianto { get; set; }
        public DateTime? DataPrimoImpianto { get; set; }
        public int? Naccumulatori { get; set; }
        public int? IdUtente { get; set; }
        public DateTime? DataInserimento { get; set; }

        public virtual UtImpianti IdImpiantoNavigation { get; set; }
        public virtual ICollection<UtBlocchiAttrezzature> UtBlocchiAttrezzature { get; set; }
    }
}
