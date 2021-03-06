using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaPGT
{
    public partial class UtCostruttori
    {
        public UtCostruttori()
        {
            UtAttrezzature = new HashSet<UtAttrezzature>();
            UtDischiRottura = new HashSet<UtDischiRottura>();
            UtModelli = new HashSet<UtModelli>();
        }

        public int Id { get; set; }
        public string Codice { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public virtual ICollection<UtAttrezzature> UtAttrezzature { get; set; }
        public virtual ICollection<UtDischiRottura> UtDischiRottura { get; set; }
        public virtual ICollection<UtModelli> UtModelli { get; set; }
    }
}
