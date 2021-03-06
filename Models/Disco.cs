using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Models
{
    public class Disco
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Numero lotto", Prompt = "Numero lotto", Description = "Numero lotto")]
        public string LottoNo { get; set; }
        [Display(Name = "Costruttore", Prompt = "Costruttore", Description = "Costruttore")]
        public string Costruttore { get; set; }
        [Display(Name = "Pressione di rottura [bar]", Prompt = "Pressione di rottura [bar]", Description = "Pressione di rottura [bar]")]
        public int? PressioneRottura { get; set; }

        [Display(Name = "Data Creazione", Prompt = "Inserire data creazione", Description = "Data Creazione")]
        public DateTime DataCreazione { get; set; }
        
        [Display(Name = "Data ultimo aggiornamento", Prompt = "Inserire data ultimo aggiornamento", Description = "Data ultimo aggiornamento")]
        public DateTime DataAggiornamento { get; set; }
        
        [Display(Name = "Tecnico ultimo aggiornamento", Prompt = "Tecnico data ultimo aggiornamento", Description = "Tecnico ultimo aggiornamento")]
        public ApplicationUser TecnicoUltimoAggiornamento { get; set; }

        [Display(Name = "Apparecchio di pressione su cui è montato", Prompt = "Apparecchio di pressione su cui è montato", Description = "Apparecchio di pressione su cui è montato")]
        public List<Pressione> PressioneMontata { get; set; }

        public List<FileDescription> Files { get; set; }
    }
}
