using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaGHI
{
    public partial class UtdTipiAttrezzature
    {
        public UtdTipiAttrezzature()
        {
            UtAttrezzature = new HashSet<UtAttrezzature>();
        }

        public int Id { get; set; }
        public string Codice { get; set; }
        public string Tipo { get; set; }
        public string Descrizione { get; set; }

        public virtual ICollection<UtAttrezzature> UtAttrezzature { get; set; }
    }
}
