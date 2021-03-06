using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaGHI
{
    public partial class UtModelli
    {
        public int Id { get; set; }
        public string NomeModello { get; set; }
        public int? IdCostruttore { get; set; }

        public virtual UtCostruttori IdCostruttoreNavigation { get; set; }
    }
}
