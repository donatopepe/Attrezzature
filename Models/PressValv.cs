using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AttrOleo.Models
{
    public class PressValv
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Area", Order = 0, Prompt = "Inserire area", Description = "Area")]
        public string Area { get; set; }
        [Display(Name = "Reparto", Order = 1, Prompt = "Inserire reparto", Description = "Reparto")]
        public string Reparto { get; set; }
        [Display(Name = "Impianto", Order = 2, Prompt = "Inserire impianto", Description = "Impianto")]
        public string Impianto { get; set; }
        [Display(Name = "Matricola", Order = 3, Prompt = "Inserire matricola", Description = "Matricola")]
        public string Matricola { get; set; }
        [Display(Name = "Costruttore", Prompt = "Costruttore", Description = "Costruttore")]
        public string Costruttore { get; set; }

        
        [Display(Name = "Data Creazione", Prompt = "Inserire data creazione", Description = "Data Creazione")]
        public DateTime DataCreazione { get; set; }
       
        [Display(Name = "Data ultimo aggiornamento", Prompt = "Inserire data ultimo aggiornamento", Description = "Data ultimo aggiornamento")]
        public DateTime DataAggiornamento { get; set; }
        [Display(Name = "Tecnico ultimo aggiornamento", Prompt = "Tecnico ultimo aggiornamento", Description = "Tecnico ultimo aggiornamento")]
        public ApplicationUser TecnicoUltimoAggiornamento { get; set; }
        [Display(Name = "Stato", Prompt = "Stato", Description = "Stato")]
        public string Stato { get; set; }

        [Display(Name = "Note", Prompt = "Note", Description = "Note")]
        public string Note { get; set; }
        public List<FileDescription> Files { get; set; }
    }
}
