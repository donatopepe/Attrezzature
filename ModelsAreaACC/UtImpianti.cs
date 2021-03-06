using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaACC
{
    public partial class UtImpianti
    {
        public UtImpianti()
        {
            UtCentrali = new HashSet<UtCentrali>();
            UtValvoleSicurezza = new HashSet<UtValvoleSicurezza>();
        }

        public int Id { get; set; }
        public string Codice { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public int? IdArea { get; set; }

        public virtual UtAree IdAreaNavigation { get; set; }
        public virtual ICollection<UtCentrali> UtCentrali { get; set; }
        public virtual ICollection<UtValvoleSicurezza> UtValvoleSicurezza { get; set; }
    }
}
