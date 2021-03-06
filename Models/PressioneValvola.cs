using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Models
{
    public class PressioneValvola
    {
        public int PressioneID { get; set; }
        public int ValvolaID { get; set; }
        [Display(Name = "Apparecchio di pressione su cui è montato", Prompt = "Apparecchio di pressione su cui è montato", Description = "Apparecchio di pressione su cui è montato")]
        public Pressione Pressione { get; set; }
        [Display(Name = "Valvola montata", Prompt = "Valvola montata", Description = "Valvola montata")]
        public Valvola Valvola { get; set; }
    }
}
