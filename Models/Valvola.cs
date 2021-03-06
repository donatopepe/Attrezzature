using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Models
{
    public class Valvola : PressValv
    {
        [Display(Name = "Modello", Prompt = "Modello", Description = "Modello")]
        public string Modello { get; set; }

        [Display(Name = "Pressione di taratura [bar]", Prompt = "Pressione di taratura [bar]", Description = "Pressione di taratura [bar]")]
        public int? PressioneTaratura { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Verifica taratura", Prompt = "Verifica taratura", Description = "Verifica taratura")]
        public DateTime? VerificaTaratura { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Scadenza taratura", Prompt = "Scadenza taratura", Description = "Scadenza taratura")]
        public DateTime? ScadenzaTaratura { get; set; }


              
        [Display(Name = "Apparecchio di pressione su cui è montato", Prompt = "Apparecchio di pressione su cui è montato", Description = "Apparecchio di pressione su cui è montato")]
        public List<PressioneValvola> PressioneMontata { get; set; }


    }
}
