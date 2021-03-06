using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaLAM
{
    public partial class UtVerificheAttrezzatureInPressione
    {
        public int Id { get; set; }
        public int? IdUtente { get; set; }
        public DateTime? DataInserimento { get; set; }
        public int? IdAttrezzatura { get; set; }
        public int? IdStato { get; set; }
        public DateTime? DataRf { get; set; }
        public DateTime? DataSf { get; set; }
        public DateTime? DataRi { get; set; }
        public DateTime? DataSi { get; set; }
        public string CertificatoFunzionalita { get; set; }
        public string CertificatoIntegrita { get; set; }
        public string CertificatoSpessore { get; set; }
        public int? IdBlocco { get; set; }
    }
}
