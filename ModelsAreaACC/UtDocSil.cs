using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaACC
{
    public partial class UtDocSil
    {
        public int IdOperazione { get; set; }
        public int? IdUtenteSil { get; set; }
        public DateTime? DataOraOperazione { get; set; }
        public int? IdImpianto { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string TipoApparecchio { get; set; }
        public string Matricola { get; set; }
        public string Note { get; set; }
        public int? StatoValidazione { get; set; }
        public string NomeFile { get; set; }
        public int? IdUtenteValidazione { get; set; }
        public DateTime? DataOraValidazione { get; set; }
    }
}
