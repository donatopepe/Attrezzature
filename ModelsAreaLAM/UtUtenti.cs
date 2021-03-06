using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaLAM
{
    public partial class UtUtenti
    {
        public int UserId { get; set; }
        public string Utente { get; set; }
        public string Password { get; set; }
        public int? Ruolo { get; set; }
        public int? IdImpianto { get; set; }
        public string CognomeNome { get; set; }
        public DateTime? DataUltimoAccesso { get; set; }
        public int? ContatoreAccessi { get; set; }
        public int? Bloccato { get; set; }
    }
}
