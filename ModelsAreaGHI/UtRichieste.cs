using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaGHI
{
    public partial class UtRichieste
    {
        public int Id { get; set; }
        public int? IdUtente { get; set; }
        public DateTime? DataRichiesta { get; set; }
        public string TipoRichiesta { get; set; }
        public string TestoRichiesta { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string CognomeNome { get; set; }
        public bool? Risposta { get; set; }
        public string TestoRisposta { get; set; }
    }
}
