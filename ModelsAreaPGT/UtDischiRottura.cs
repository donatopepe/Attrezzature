using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaPGT
{
    public partial class UtDischiRottura
    {
        public UtDischiRottura()
        {
            UtAttrezzature = new HashSet<UtAttrezzature>();
        }

        public int Id { get; set; }
        public string Lotto { get; set; }
        public int IdCostruttore { get; set; }
        public int? Pressione { get; set; }
        public bool? Documentazione { get; set; }
        public int? IdUtente { get; set; }
        public DateTime? DataInserimento { get; set; }
        public int? IdImpianto { get; set; }
        public string Certificato { get; set; }

        public virtual UtCostruttori IdCostruttoreNavigation { get; set; }
        public virtual ICollection<UtAttrezzature> UtAttrezzature { get; set; }
    }
}
