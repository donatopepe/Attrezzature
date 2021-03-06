using System;
using System.Collections.Generic;

namespace AttrOleo.ModelsAreaACC
{
    public partial class UtAree
    {
        public UtAree()
        {
            UtImpianti = new HashSet<UtImpianti>();
            UtValvoleSicurezza = new HashSet<UtValvoleSicurezza>();
        }

        public int IdArea { get; set; }
        public string Area { get; set; }

        public virtual ICollection<UtImpianti> UtImpianti { get; set; }
        public virtual ICollection<UtValvoleSicurezza> UtValvoleSicurezza { get; set; }
    }
}
