using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaGHI
{
    public partial class UtTaratureValvoleSicurezza
    {
        public UtTaratureValvoleSicurezza()
        {
            UtrValvoleSicurezzaTaratureValvoleSicurezza = new HashSet<UtrValvoleSicurezzaTaratureValvoleSicurezza>();
        }

        public int Id { get; set; }
        public DateTime Data { get; set; }
        public DateTime Scadenza { get; set; }
        public string Note { get; set; }

        public virtual ICollection<UtrValvoleSicurezzaTaratureValvoleSicurezza> UtrValvoleSicurezzaTaratureValvoleSicurezza { get; set; }
    }
}
