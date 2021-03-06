using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaLAM
{
    public partial class UtrValvoleSicurezzaTaratureValvoleSicurezza
    {
        public int IdValvolaSicurezza { get; set; }
        public int IdTaratura { get; set; }

        public virtual UtTaratureValvoleSicurezza IdTaraturaNavigation { get; set; }
        public virtual UtValvoleSicurezza IdValvolaSicurezzaNavigation { get; set; }
    }
}
